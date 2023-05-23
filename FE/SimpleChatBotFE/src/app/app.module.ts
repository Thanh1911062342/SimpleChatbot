import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './modules/login/login.component';
import { ChatbotComponent } from './modules/chatbot/chatbot.component';
import { AuthService } from './business/services/authService';
import { AuthGuard } from './business/services/authGuard';
import { CookieService } from 'ngx-cookie-service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtInterceptor } from './business/interceptors/jwtInterceptor';
import { ChatBotService } from './business/services/chatbotService';
import { StorageService } from './business/services/storageService';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ChatbotComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    CookieService,
    AuthService,
    ChatBotService,
    StorageService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
