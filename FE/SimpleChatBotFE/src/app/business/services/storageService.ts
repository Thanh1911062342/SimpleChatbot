import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Route, Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { ILogin } from '../models/login';

@Injectable({
    providedIn: 'root'
})
export class StorageService {

    constructor(private cookieService: CookieService,
        private router: Router,
        protected httpClient: HttpClient) {

    }
    
    saveToStorage(item: string, data: any) {
        const serializedData = JSON.stringify(data);
        localStorage.setItem(`${item}`, serializedData);
    }

    getItemFromStorage(key: string): string {
        let value = localStorage.getItem(key);
        if (value != null){
            return value.toString();
        }

        return '';
    }

    removeFromStorage(item: string){
        localStorage.removeItem(item);
        this.router.navigate(['/login']);
    }
}