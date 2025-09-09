import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './customer/product-list/product-list.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterShopkeeperComponent } from './auth/register-shopkeeper/register-shopkeeper.component';
import { RegisterCustomerComponent } from './auth/register-customer/register-customer.component';
import { RegisterDeliveryStaffComponent } from './auth/register-delivery/register-delivery.component';
import { CustomerPageComponent } from './customer-page/customer-page.component';
import { SearchProductsComponent } from './components/search-products/search-products.component';
import { CartComponent } from './cart/cart.component';
import { PaymentComponent } from './payment/payment.component';
import { StorekeeperDashboardComponent } from './storekeeper-dashboard/storekeeper-dashboard.component';
import { AddProductComponent } from './add-product/add-product.component';
import { AssignOrdersComponent } from './assign-orders/assign-orders.component';
import { DeliveryDashboardComponent } from './delivery-dashboard/delivery-dashboard.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { LogsComponent } from './components/logs/logs.component';
import { DeleteUserComponent } from './components/delete-user/delete-user.component';
import { SuperAdminDashboardComponent } from './components/super-admin-dashboard/super-admin-dashboard.component';
import { AddAdminComponent } from './components/add-admin/add-admin.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register/customer', component: RegisterCustomerComponent },
  { path: 'register/shopkeeper', component: RegisterShopkeeperComponent },
  { path: 'register/delivery', component: RegisterDeliveryStaffComponent },
  { path: 'customer', component: CustomerPageComponent },
  { path: 'Home', component:ProductListComponent},
  { path: 'search-products', component: SearchProductsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'search', component: SearchProductsComponent },
  { path: 'payment', component: PaymentComponent },
  { path: 'storekeeper', component: StorekeeperDashboardComponent },
  { path: 'storekeeper/add-product', component: AddProductComponent },
  { path: 'assign-orders', component: AssignOrdersComponent },
  { path: 'delivery-dashboard', component: DeliveryDashboardComponent},
  { path: 'admin-dashboard', component: AdminDashboardComponent },
  { path: 'logs', component: LogsComponent },
  { path: 'delete-user', component: DeleteUserComponent },
  { path: 'super-admin-dashboard', component: SuperAdminDashboardComponent },
  { path: 'add-admin', component: AddAdminComponent },
   { path: 'my-orders', component: MyOrdersComponent },
  { path: '', redirectTo: '/Home', pathMatch: 'full' }, 
  { path: '**', redirectTo: '/Home' } ,
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
