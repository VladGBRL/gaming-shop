using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.AccountManagement.DTOs
{
    public class RegisterDTO
    {

        [Required]
        [StringLength(20, ErrorMessage = "First name can't be longer than 20 characters!")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(20, ErrorMessage = "Last name can't be longer than 20 characters!")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
