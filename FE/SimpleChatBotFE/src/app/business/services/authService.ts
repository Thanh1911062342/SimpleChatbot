import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Route, Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { ILogin } from '../models/login';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private cookieService: CookieService,
        private router: Router,
        protected httpClient: HttpClient) {

    }

    checkLoginStatus(): boolean {
        return localStorage.getItem("LoggedIn") !== null;
    }

    login(data: ILogin): Observable<any> {
        return this.httpClient.post<any>(`https://localhost:9998/api/account/login`, data, {
            headers: {
                "content-type": "application/json",
            },
            observe: "response",
        });
    }

    keyActive(email: string): Observable<any> {
        let params = new HttpParams();
        params = params.append('email', email);

        return this.httpClient.get<any>(`https://localhost:9998/api/account/keyActive`, {
            params: params,
            headers:{
                'content-type': 'application/json',
            }
        })
    }

    checkSpamGetKey(): Observable<any> {
        return this.httpClient.get<any>(`https://localhost:9998/api/account/checkSpamGetKey`, {
            headers:{
                'content-type': 'application/json',
            },
        })
    }
}