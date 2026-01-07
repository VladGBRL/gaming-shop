import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { LoginComponent } from './features/pages/login/login.component';
import { RegisterComponent } from './features/pages/register/register.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HomeComponent } from './features/pages/home/home.component';
import { ProductsComponent } from './features/pages/products/products.component';
import { AdminDashboardComponent } from './features/pages/admin-dashboard/admin-dashboard.component';
import { WishlistComponent } from './features/pages/wishlist/wishlist.component';
import { CartComponent } from './features/pages/cart/cart.component';
import { SuccessComponent } from './features/pages/success/success.component';
import { CancelComponent } from './features/pages/cancel/cancel.component';
import { HistoryComponent } from './features/pages/history/history.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ProductsComponent,
    AdminDashboardComponent,
    WishlistComponent,
    CartComponent,
    SuccessComponent,
    CancelComponent,
    HistoryComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
