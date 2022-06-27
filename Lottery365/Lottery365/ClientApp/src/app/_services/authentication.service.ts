import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDetails } from '../_models/userDetails';

//import { environment } from '@environments/environment';
//import { User } from '';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<UserDetails>;
  public currentUser: Observable<UserDetails>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<UserDetails>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserDetails {
    return this.currentUserSubject.value;
  }

  login(model: any) {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>('https://localhost:5001/api/Accounts/UserLogin', model, httpOptions)
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user) {
          // store user details local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
