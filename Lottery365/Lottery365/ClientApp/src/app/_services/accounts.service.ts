import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserLotteryDetails } from '../_models/UserLotteryDetails';
import { callbackify } from 'util';
import { AdminDrawDetails } from '../_models/admindrawDetails';
import { UserDetails } from '../_models/userDetails';
import { WheelDetails } from '../_models/wheelDetails';



@Injectable({ providedIn: 'root' })
export class AccountService {
  Url: string;
  token: string;
  header: any;
  activeWheelNumber: number;
  adminDrawNumbers: number[];
  allUsers: UserDetails[];
  wheelNumberToInsert: number;
  wheelDetails: WheelDetails[];

  constructor(private http: HttpClient) {
    this.Url = window.location.origin + '/api/Accounts/';
  }

  Login(model: any) {

    var a = this.Url + 'UserLogin';
    return this.http.post<any>(this.Url + 'UserLogin', model, { headers: this.header });
  }
  CreateUser(register: any) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.Url + 'CreateUser', register, httpOptions)
  }

  CreateWheel(wheel: any) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.Url + 'CreateWheelInfo', wheel, httpOptions)
  }

  SaveUserLotteryDetails(details: UserLotteryDetails) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.Url + 'AddUserLotteryDetails', details, httpOptions);
  }

  SaveAdminDrawNumberDetails(details: AdminDrawDetails) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.Url + 'AddWheelInfo', details, httpOptions);
  }

  editUser(details: any) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.Url + 'UpdateUser', details, httpOptions);
  }

  async getactiveWheelNumber() {
    //this.http.get(this.Url + 'ActiveWheel').subscribe(data => {
    //  this.activeWheelNumber = data as number;
    //   callback(this.activeWheelNumber);
    //});
    return this.http.get<number>(this.Url + 'ActiveWheel').toPromise();
  }


  getadminDrawNumbers(callback) {
    this.http.get(this.Url + 'AdminDrawNumbers').subscribe(data => {
      this.adminDrawNumbers = data as number[];
      callback(this.adminDrawNumbers);
    });
  }

  getAllUsers(callback) {
    this.http.get(this.Url + 'AllUsers').subscribe(data => {
      this.allUsers = data as UserDetails[];
      callback(this.allUsers);
    });
  }

  getWheelNumberForInsert(callback) {
    this.http.get(this.Url + 'GetWheelNumberForInsert').subscribe(data => {
      this.wheelNumberToInsert = data as number;
      callback(this.wheelNumberToInsert);
    });
  }

  getWheelDetails(callback) {
    this.http.get(this.Url + 'WheelDetails').subscribe(data => {
      this.wheelDetails = data as WheelDetails[];
      callback(this.wheelDetails);
    });
  }

  async checkIfUserAlreadyHasLotteryNumber(userId: number, wheelNumber: number) {
    return this.http.get<boolean>(this.Url + 'CheckIfUserHasLotteryNumber?userId=' + userId + '&wheelNumber=' + wheelNumber).toPromise();
  }
}
