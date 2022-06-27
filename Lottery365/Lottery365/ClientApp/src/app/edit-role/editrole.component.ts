import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { Role } from '../_models/role';
import { AccountService } from '../_services/accounts.service';

@Component({
  selector: 'app-edit-role',
  templateUrl: './editrole.component.html',
  styleUrls: ['./editrole.component.css']
})
export class EditRoleComponent {

  constructor(private accountService: AccountService) {

  }

  @ViewChild('myModal', { static: false }) modal: ElementRef;
  usertoUpdate: any;
  userId: any;
  name: any;
  changedRole: any;
  errorMessage: string;
  open(user: any) {
    this.usertoUpdate = user;
    this.userId = user.userId;
    this.name = user.firstName + ' ' + user.lastName;
    if (user.role == Role.Admin) {
      this.changedRole = "User";
    }
    else if (user.role == Role.User) {
      this.changedRole = "Admin";
    }
    this.modal.nativeElement.style.display = 'block';
  }

  updateRole() {
    this.usertoUpdate.role = this.changedRole;
    this.accountService.editUser(this.usertoUpdate).subscribe(
      data => {
        if (data['status'] == 'Success') {
          this.modal.nativeElement.style.display = 'none';
        }
        else {
          this.errorMessage = data['errorMessage'];
        }
      },
      error => {
        this.errorMessage = error.message;
      });
  }

  close() {
    this.modal.nativeElement.style.display = 'none';
  }
}
