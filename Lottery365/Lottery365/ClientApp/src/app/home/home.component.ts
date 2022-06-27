import { Component } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/accounts.service';
import { NavigationExtras } from '@angular/router';
import { UserDetails } from '../_models/userDetails';
import { filter, first, map } from 'rxjs/operators';
import { AuthenticationService } from '../_services/authentication.service';
import { Role } from '../_models/role';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  model: any = {};

  errorMessage: string;
  constructor(private router: Router, private accountService: AccountService, private authenticationService: AuthenticationService) { }


  ngOnInit() {

  }

  login({ value, valid }) {
    if (valid) {
   
      this.authenticationService.login(this.model).pipe(first()).subscribe(
        data => {
        
          if (data != null) {
            let user: UserDetails = { userId: data['userId'], firstName: data['firstName'], lastName: data['lastName'], emailId: data['emailId'], role: data['role'] };
            let navigationExtras: NavigationExtras = {
              state: {
                user: user
              }
            };
            if (user.role == Role.User)
              this.router.navigate(['/ticket-list'], navigationExtras);
            if (user.role == Role.Admin)
              this.router.navigate(['/admin-wheel-info']);
          }
          else {
            this.errorMessage = "Invalid User";
          }
        },
        error => {
          this.errorMessage = error.message;
        });
    };
  }
}
