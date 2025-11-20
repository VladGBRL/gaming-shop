import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/pages/login/login.component';
import { RegisterComponent } from './features/pages/register/register.component';
import { HomeComponent } from './features/pages/home/home.component';
import { ProductsComponent } from './features/pages/products/products.component';
import { AdminDashboardComponent } from './features/pages/admin-dashboard/admin-dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
