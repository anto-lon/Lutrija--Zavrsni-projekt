import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/accounts.service';
import { HttpClient } from '@angular/common/http';
import { UserLotteryDetails } from '../_models/UserLotteryDetails';
import { AdminDrawDetails } from '../_models/admindrawDetails';


@Component({
  selector: 'admin-generate-ticket',
  templateUrl: './admingenerateticket.component.html',
})
export class AdminGenerateTicketComponent {
  constructor(private accountService: AccountService, private router: Router) { }
  errorMessage: string;
  successMessage: string;
  adminDrawNumbers: number[]
  showSaveButton = false;
  wheelNumberToInsert: number;

  async ngOnInit() {
   
    this.wheelNumberToInsert = await this.accountService.getactiveWheelNumber();
    
  }

  getAdminDrawNumbers() {
    this.accountService.getadminDrawNumbers(() => {
      this.adminDrawNumbers = this.accountService.adminDrawNumbers;
      this.showSaveButton = true;
    });
  }

  saveAdminLotteryNumbers() {

    var wheelNumber = this.wheelNumberToInsert;
    const details: AdminDrawDetails = {
      drawNumbers : this.adminDrawNumbers.join(' '),
      wheelNumber : wheelNumber
    }

    this.accountService.SaveAdminDrawNumberDetails(details).subscribe(
      data => {
      
        if (data['status'] == 'Success') {        
          this.successMessage = 'details saved successfully';
          this.router.navigate(['/admin-wheel-info']);
        }
        else {
          this.errorMessage = data['errorMessage'];
        }
      },
      error => {
        this.errorMessage = error.message;
        console.log(this.errorMessage);
      });
  }

}
