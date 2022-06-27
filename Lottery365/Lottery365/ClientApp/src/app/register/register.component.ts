import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/accounts.service';
import { Observable } from 'rxjs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  model: any = {} ;

  errorMessage: string;
  constructor(private router: Router, private accountService: AccountService) { }


  ngOnInit() {
    
  }
  createUser({ value, valid }) {
    if (valid) {
      debugger;
      this.accountService.CreateUser(this.model).subscribe(
        data => {
         
          if (data['status'] == 'Success') {
            this.router.navigate(['login']);
            debugger;
          }
          else {
            this.errorMessage = data['errorMessage'];
          }
        },
        error => {
          this.errorMessage = error.message;
        });
    };
  }
}
