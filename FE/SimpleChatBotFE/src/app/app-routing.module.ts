import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/login/login.component';
import { ChatbotComponent } from './modules/chatbot/chatbot.component';
import { AuthGuard } from './business/services/authGuard';

const routes: Routes = 
[
  {path: 'login', component: LoginComponent},
  {path: '', component: ChatbotComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
