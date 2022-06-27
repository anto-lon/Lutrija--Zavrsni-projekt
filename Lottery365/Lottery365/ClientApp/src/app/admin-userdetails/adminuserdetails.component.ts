import { Component, Input, ViewChild } from '@angular/core';
import { ActivatedRoute, Navigation, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/accounts.service';
import { HttpClient } from '@angular/common/http';
import { UserDetails } from '../_models/userDetails';
import { EditRoleComponent } from '../edit-role/editrole.component';


@Component({
  selector: 'admin-user-details',
  templateUrl: './adminuserdetails.component.html',
})
export class AdminUserDetails {
  constructor(private httpService: HttpClient, private accountService: AccountService) {
  
  }

  userDetails: UserDetails[];
  //userId: any;
  ngOnInit() {
    this.accountService.getAllUsers(() => {
      this.userDetails = this.accountService.allUsers;
    });
  }

  @ViewChild('modal', { static: false }) modal: EditRoleComponent

  openModal(user) {
    //this.userId = user.userId;
    this.modal.open(user);
  }
  
}

