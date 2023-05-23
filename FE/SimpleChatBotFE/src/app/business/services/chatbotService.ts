import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
    providedIn: 'root'
})
export class ChatBotService {
    constructor(private cookieService: CookieService,
        private router: Router,
        protected httpClient: HttpClient) { }

    sendMessage(data: string): Observable<any>{
        return this.httpClient.post<any>(`https://localhost:9998/api/chatbot/sendMessage`, data, {
            headers: {
                "content-type": "application/json",
            },
            observe: "response",
        });
    }
}