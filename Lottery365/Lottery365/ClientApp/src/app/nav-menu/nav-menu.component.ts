import { Component } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { Role } from '../_models/role';
import { UserDetails } from '../_models/userDetails';
import { AccountService } from '../_services/accounts.service';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser: UserDetails;
  activeWheelNumber: number;
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService, private accountService: AccountService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }


  ngOnInit() {

  }

  get isAdmin() {
    return this.currentUser && this.currentUser.role === Role.Admin;
  }

  get isUser() {
    return this.currentUser && this.currentUser.role == Role.User;
  }

  async generateUserLotteryTicket() {

    this.activeWheelNumber = await this.accountService.getactiveWheelNumber();
    if (this.activeWheelNumber == undefined || this.activeWheelNumber == null || this.activeWheelNumber == 0) {
      alert('No Active Lottery Avaiable');
      return;
    }
    let navigationExtras: NavigationExtras = {
      state: {
        userId: this.currentUser.userId
      }
    };
    var lotteryTicketExists = await this.accountService.checkIfUserAlreadyHasLotteryNumber(Number(this.currentUser.userId), this.activeWheelNumber);
    if (lotteryTicketExists) {
      alert('Lottery Ticket has already been generated');
    }
    else {
      this.router.navigate(['/generate-userticket'], navigationExtras);
    }
  }

  async navigatetoAdminDrawNumber() {

    this.activeWheelNumber = await this.accountService.getactiveWheelNumber();
    if (this.activeWheelNumber == undefined || this.activeWheelNumber == null || this.activeWheelNumber == 0) {
      alert('No Active Lottery Avaiable');
      return;
    }
    this.router.navigate(['/admin-generate-ticket']);
  }
  async navigateToWheelNumber() {
    this.activeWheelNumber = await this.accountService.getactiveWheelNumber();
    if (this.activeWheelNumber > 0) {
      alert('Active Lottery is already running');
      return;
    }
    this.router.navigate(['/generate-wheel-number']);
  }

  navigateToHome() {
    if (this.isAdmin) {
      this.router.navigate(['/admin-wheel-info']);
    }
    else {
      let navigationExtras: NavigationExtras = {
        state: {
          user: this.currentUser
        }
      };
      this.router.navigate(['/ticket-list'], navigationExtras);
    }
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
