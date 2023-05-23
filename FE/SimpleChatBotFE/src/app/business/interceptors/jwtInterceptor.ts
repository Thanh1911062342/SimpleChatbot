import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storageService';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(private storageService: StorageService){}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const jwt = this.storageService.getItemFromStorage('LoggedIn');

            // Clone request để thay đổi headers
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${jwt}`
                }
            });

        // Gửi request đã chỉnh sửa tới next handler trong chuỗi
        return next.handle(request);
    }
}