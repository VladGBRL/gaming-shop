import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/pages/login/login.component';
import { RegisterComponent } from './features/pages/register/register.component';
import { HomeComponent } from './features/pages/home/home.component';
import { ProductsComponent } from './features/pages/products/products.component';
import { AdminDashboardComponent } from './features/pages/admin-dashboard/admin-dashboard.component';
import { WishlistComponent } from './features/pages/wishlist/wishlist.component';
import { CartComponent } from './features/pages/cart/cart.component';
import { SuccessComponent } from './features/pages/success/success.component';
import { CancelComponent } from './features/pages/cancel/cancel.component';
import { HistoryComponent } from './features/pages/history/history.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent },
  { path: 'wishlist', component: WishlistComponent },
  { path: 'cart', component: CartComponent },
  { path: 'success', component: SuccessComponent },
  { path: 'cancel', component: CancelComponent },
  { path: 'history', component: HistoryComponent } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
