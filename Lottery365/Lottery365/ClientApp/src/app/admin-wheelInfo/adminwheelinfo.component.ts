import { Component, Input, ViewChild } from '@angular/core';
import { ActivatedRoute, Navigation, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/accounts.service';
import { HttpClient } from '@angular/common/http';
import { UserDetails } from '../_models/userDetails';
import { EditRoleComponent } from '../edit-role/editrole.component';
import { WheelDetails } from '../_models/wheelDetails';


@Component({
  selector: 'admin-wheel-info',
  templateUrl: './adminwheelinfo.component.html',
})
export class AdminWheelInfoComponent {
  constructor(private httpService: HttpClient, private accountService: AccountService,private router: Router) {
  
  }

  wheelInfo: WheelDetails[];

  ngOnInit() {
    this.accountService.getWheelDetails(() => {
      this.wheelInfo = this.accountService.wheelDetails;
    });
  }  
}

