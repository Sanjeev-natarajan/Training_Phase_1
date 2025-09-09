import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductListComponent } from './customer/product-list/product-list.component';
import { LoginComponent } from './auth/login/login.component';
import { RoleNavbarComponent } from './role-navbar/role-navbar.component';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RegisterCustomerComponent } from './auth/register-customer/register-customer.component';
import { RegisterShopkeeperComponent } from './auth/register-shopkeeper/register-shopkeeper.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegisterDeliveryStaffComponent } from './auth/register-delivery/register-delivery.component';
import { CustomerPageComponent } from './customer-page/customer-page.component';
import { SearchProductsComponent } from './components/search-products/search-products.component';
import { CartComponent } from './cart/cart.component';
import { PaymentComponent } from './payment/payment.component';
import { StorekeeperDashboardComponent } from './storekeeper-dashboard/storekeeper-dashboard.component';
import { AddProductComponent } from './add-product/add-product.component';
import { ProductEditDialogComponent } from './storekeeper/product-edit-dialog/product-edit-dialog.component';
import { AssignOrdersComponent } from './assign-orders/assign-orders.component';
import { DeliveryDashboardComponent } from './delivery-dashboard/delivery-dashboard.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { LogsComponent } from './components/logs/logs.component';
import { DeleteUserComponent } from './components/delete-user/delete-user.component';
import { SuperAdminDashboardComponent } from './components/super-admin-dashboard/super-admin-dashboard.component';
import { AddAdminComponent } from './components/add-admin/add-admin.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';



@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    LoginComponent,
    
    RoleNavbarComponent,
    NavbarComponent,
    FooterComponent,
    RegisterCustomerComponent,
    RegisterShopkeeperComponent,
    RegisterDeliveryStaffComponent,
    CustomerPageComponent,
    SearchProductsComponent,
    CartComponent,
    PaymentComponent,
    StorekeeperDashboardComponent,
    AddProductComponent,
    ProductEditDialogComponent,
    AssignOrdersComponent,
    DeliveryDashboardComponent,
    AdminDashboardComponent,
    LogsComponent,
    DeleteUserComponent,
    SuperAdminDashboardComponent,
    AddAdminComponent,
    MyOrdersComponent
    
      ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot([]),
    ReactiveFormsModule,
    HttpClientModule,
     FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
