import { Component } from '@angular/core';
import { ActivatedRoute, Navigation, NavigationExtras, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/accounts.service';
import { HttpClient } from '@angular/common/http';
import { UserLotteryDetails } from '../_models/UserLotteryDetails';


@Component({
  selector: 'generate-user-ticket',
  templateUrl: './generateuserticket.component.html',
})
export class GenerateUserTicketComponent {
  constructor(private accountService: AccountService, private router: Router) {
    let nav: Navigation = this.router.getCurrentNavigation();
    if (nav.extras && nav.extras.state && nav.extras.state.userId) {
      this.id = nav.extras.state.userId;
    }
  }
  id: string;
  errorMessage: string;
  successMessage: string;
  activeWheelNumber: number;
  async ngOnInit() {
    this.activeWheelNumber = await this.accountService.getactiveWheelNumber();
  }

  preventInput(inputNumber) {
    var maxValue = 49;
    var minValue = 1;
    if (Number(inputNumber.value) < minValue || Number(inputNumber.value) > maxValue) {
      alert('please enter numbers between 1 and 49');
      inputNumber.value = '';
    }
  }

  async saveNumbers(val1: number, val2: number, val3: number, val4: number, val5: number, val6: number) {

    var lotteryTicketExists = await this.accountService.checkIfUserAlreadyHasLotteryNumber(Number(this.id), this.activeWheelNumber);
    if (lotteryTicketExists) {
      alert('Lottery Ticket has already been generated');

    }
    else {
      var drawnumbersString = [val1, val2, val3, val4, val5, val6];
      let findDuplicates = arr => arr.filter((item, index) => arr.indexOf(item) != index)
      var duplicates = [...new Set(findDuplicates(drawnumbersString))];
      if (duplicates.length != 0) {
        alert('Numbers ' + duplicates.join(' ') + ' have been entered more than once.Please ensure that each lottery number is unique');
        return;
      }
        var drawNumbers = drawnumbersString.join(' ');
      //check if ticket number does not exist
      var ticketNumber = Math.floor(Math.random() * 90000) + 10000;
      const details: UserLotteryDetails = {
        drawNumbers: drawNumbers,
        status: '',
        ticketNumber: ticketNumber,
        userId: Number(this.id),
        wheelNumber: this.activeWheelNumber
      }

      this.accountService.SaveUserLotteryDetails(details).subscribe(
        data => {

          if (data['status'] == 'Success') {
            //  alert('details saved successfully');
            this.successMessage = 'details saved successfully';

            let navigationExtras: NavigationExtras = {
              state: {
                userId: this.id
              }
            };
            this.router.navigate(['/generate-userticket'], navigationExtras);
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
}
