import { Component, Input } from '@angular/core';
import { ActivatedRoute, Navigation, NavigationExtras, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/accounts.service';
import { HttpClient } from '@angular/common/http';
import { UserDetails } from '../_models/userDetails';


@Component({
  selector: 'app-ticket-list',
  templateUrl: './userticket.component.html',
})
export class UserTicketsComponent {
  constructor(private httpService: HttpClient, private accountsService: AccountService, private router: Router) {
    let nav: Navigation = this.router.getCurrentNavigation();
    if (nav.extras && nav.extras.state && nav.extras.state.user) {
      this.user = nav.extras.state.user;
    }

    this.headertoPrint = 'Lottery 365';
    this.addresstoPrint = 'Lutrija d.o.o, Ulica grada Mostara 82';
  }
  tickets: string[];
  id: string;
  source: any;
  user: UserDetails;
  firstName: string;
  lastName: string;
  email: string;
  headertoPrint: string;
  addresstoPrint: string;
  ngOnInit() {
    this.firstName = this.user.firstName;
    this.lastName = this.user.lastName;
    this.email = this.user.emailId;
    this.httpService.get(window.location.origin + '/api/Accounts/UserTicketDetails?userId=' + this.user.userId).subscribe(
      data => {
        this.tickets = data as string[];
      }
    );
  }

  printTicket(ticket) {
    let dataToPrint = '<br />' + '<div class="center">' + '<h2>' + this.headertoPrint + '</h2>' + '<h3>' + this.addresstoPrint + '</h3>' + '</div>' +  '<hr />'
      + '<span class="il">' + '<h4>' + 'Igra: Loto' +
      '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; '
      + 'Kolo: ' + ticket.wheelNumber + '</h4>' + '<hr />' + '<hr />' + '<br />'
      + '<table>' +
      '<tr>' +
      '<th>' + 'Draw Numbers' + '</th>' +
      '</tr>' +
      '<tr>' +
      '<td>' + ticket.drawNumbers + '</td>' +
      '</tr>' +
      '</table>' + '<br />' + '<div>' + ticket.ticketNumber + '  ' + '  ' + '  ' + ' </div>'

    let htmlToPrint =
      '<style type="text/css">' +
      'div {' + 'font-family: arial, sans-serif;' + '}' +
      'span {' + 'font-family: arial, sans-serif;' + '}' +
      'table {' + 'font-family: arial, sans-serif;' +
      'border-collapse: collapse;' + 'width: 95%;' +
      'margin-left: 20px' + '}' +
      'th, td {' +
      'text-align: center;' +
      'border:1px solid #000;' +
      'padding: 8px;' +
      '}' + 'tr:nth-child(even) {' +
      'background-color: #dddddd;' + '}' +
      '.center {' +
      'text-align: center;' +
      'color: blue;' +
      '.il {' +
      'display: inline;' +
      '}' +
      '</style>';
    htmlToPrint += dataToPrint;
    let windowToPrint = window.open("");
    windowToPrint.document.write(htmlToPrint);
    windowToPrint.print();
    windowToPrint.close();
  }


}

