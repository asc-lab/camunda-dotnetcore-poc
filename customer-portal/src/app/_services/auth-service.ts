import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from '../_model/login.dto';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../_model/user.dto';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    
    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username:string, password:string) {
        return this.http.post<User>('http://localhost:6060/api/user', {login: username, password: password})
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}