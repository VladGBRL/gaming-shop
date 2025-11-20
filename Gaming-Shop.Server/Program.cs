using Gaming_Shop.AccountManagement.Data;
using Gaming_Shop.AccountManagement;
using Microsoft.EntityFrameworkCore;
using Gaming_Shop.AccountManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Gaming_Shop.AccountManagement.Helpers;
using Gaming_Shop.ShopManagement.Data;
using Gaming_Shop.ShopManagement;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.SignIn.RequireConfirmedEmail = false;
})
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<AccountManagementDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenKey = "best-secure-token--best-secure-token--best-secure-token--best-secure-token--best-secure-token--best-secure-token";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };

    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddDbContext<AccountManagementDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ShopManagementDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAccountManagementModule(builder.Configuration);
builder.Services.AddShopManagementModule();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7239", "https://127.0.0.1:7239", "https://127.0.0.1:57164", "https://localhost:5161")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();


    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while populating the database.");
}

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
