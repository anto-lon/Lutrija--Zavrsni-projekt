import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AccountService } from './_services/accounts.service';
import { RegisterComponent } from './register/register.component';
import { UserTicketsComponent } from './usertickets/userticket.component';
import { GenerateUserTicketComponent } from './generateuserticket/generateuserticket.component';
import { AdminGenerateTicketComponent } from './admin-generateticket/admingenerateticket.component';
import { AdminUserDetails } from './admin-userdetails/adminuserdetails.component';
import { EditRoleComponent } from './edit-role/editrole.component';
import { AdminDrawDetails } from './_models/admindrawDetails';
import { AdminWheelInfoComponent } from './admin-wheelInfo/adminwheelinfo.component';
import { AuthenticationService } from './_services/authentication.service';
import { Role } from './_models/role';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { AuthGuard } from './_helpers/authGuard';
import { GenerateWheelNumberComponent } from './generatewheelnumber/generatewheelnumber.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    RegisterComponent,
    UserTicketsComponent,
    GenerateUserTicketComponent,
    AdminGenerateTicketComponent,
    AdminUserDetails,
    EditRoleComponent,
    AdminWheelInfoComponent,
    GenerateWheelNumberComponent
  ],



  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: HomeComponent, pathMatch: 'full' },
      { path: 'app-edit-role', component: EditRoleComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin]} },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'new-user', component: RegisterComponent },
      {
        path: 'ticket-list', component: UserTicketsComponent, canActivate: [AuthGuard]
      },
      { path: 'generate-userticket', component: GenerateUserTicketComponent, canActivate: [AuthGuard] },
      { path: 'admin-generate-ticket', component: AdminGenerateTicketComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] }  },
      { path: 'admin-user-details', component: AdminUserDetails, canActivate: [AuthGuard], data: { roles: [Role.Admin] }  },
      { path: 'admin-wheel-info', component: AdminWheelInfoComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: 'generate-wheel-number', component: GenerateWheelNumberComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } }
    ])
  ],
  providers: [AccountService, AuthenticationService, { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },],
  bootstrap: [AppComponent]
})
export class AppModule { }

