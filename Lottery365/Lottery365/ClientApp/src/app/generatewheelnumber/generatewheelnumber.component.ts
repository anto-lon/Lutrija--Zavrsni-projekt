import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/accounts.service';
import { Observable } from 'rxjs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'generate-wheel-number',
  templateUrl: './generatewheelnumber.component.html'
})
export class GenerateWheelNumberComponent {
  model: any = {} ;

  errorMessage: string;
  wheelNumber: any;
  constructor(private router: Router, private accountService: AccountService) { }


  ngOnInit() {
    this.accountService.getWheelNumberForInsert(() => {
      this.wheelNumber = this.accountService.wheelNumberToInsert;
    });
  }
  createWheel() {
    
    this.model['wheelNumber'] = this.wheelNumber;
    this.model['active'] = true;
    this.accountService.CreateWheel(this.model).subscribe(
      data => {
      
        if (data['status'] == 'Success') {
          this.errorMessage = 'success';
          this.router.navigate(['/admin-wheel-info']);
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
