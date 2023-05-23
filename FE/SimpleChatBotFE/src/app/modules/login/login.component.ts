import { AfterContentInit, AfterViewInit, Component, ElementRef, OnInit, QueryList, Renderer2, ViewChild, ViewChildren } from '@angular/core';
import { FormControl, FormGroup, Validators  } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/business/services/authService';
import { StorageService } from 'src/app/business/services/storageService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  //styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, AfterViewInit {

  loginForm!: FormGroup;

  emailInput: string = '';
  emailError: string = '';

  keyInput: string = '';
  keyError: string = '';

  @ViewChild('getKeyButton', { static: false }) getKeyButton!: ElementRef;

  constructor(private authService: AuthService,
              private renderer: Renderer2,
              private router: Router,
              private storageService: StorageService) {

  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.compose([
        Validators.required
      ])),
      key: new FormControl('', Validators.compose([
        Validators.required
      ])),
    });

    this.checkGetKeyTimeLimit();
  }

  ngAfterViewInit(): void {
  }

  onSubmit() {
    if (this.loginForm.valid){
      this.authService.login(this.loginForm.value).subscribe(
        {
          next: (res) => {
            if (res.body.notification.status == true){
              this.storageService.saveToStorage('LoggedIn', res.body.notification.message);
              this.router.navigate(['/']);
            }
          },
          error: (error) => {

          }
        }
      )
    }
    else{
      this.emailInputChecker();
      this.keyInputChecker();
    }
  }

  getKey(){
    if (!this.emailInputChecker()){
      return;
    }

    this.authService.keyActive(this.emailInput).subscribe((res: any) => {
      if (res['notification'].status == true){
        this.disactiveGetKeyButton(res.unixTimestamp);
      }
      else{

      }
    });
  }

  emailInputChecker(): boolean{
    if (this.emailInput.trim() === ''){
      this.emailError = "Không được để trống";
      return false;
    }

    if (!this.emailRegexValidator(this.emailInput)){
      this.emailError = "Vui lòng điền email phù hợp";
      return false;
    }

    this.emailError = "";

    return true;
  }

  keyInputChecker(): boolean{
    if (this.keyInput.trim() === ''){
      this.keyError = "Không được để trống";

      return false;
    }

    this.keyError = "";

    return true;
  }

  emailRegexValidator(value: string): boolean
  {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const valid = emailPattern.test(value);
    return valid ? true : false;
  }

  setErrorMessage(element: ElementRef, message: string)
  {
    element.nativeElement.textContent = message;
    this.renderer.addClass(element.nativeElement, 'Active');
  }

  checkGetKeyTimeLimit()
  {
    this.authService.checkSpamGetKey().subscribe((res: any) => {
      this.disactiveGetKeyButton(res.unixTimestamp);
    })
  }

  disactiveGetKeyButton(time: number){
    if (time >= 0){
      this.getKeyButton.nativeElement.classList.remove('Active');
      this.getKeyButton.nativeElement.classList.add('Disactive');
      this.getKeyButton.nativeElement.disabled = true;
      this.countTimer(time);
    }
  }

  countTimer(time: number){
    this.getKeyButton.nativeElement.innerHTML = time.toString();
    time--;

    if (time >= 0){
      setTimeout(() => this.countTimer(time), 982);
    }
    else{
      this.getKeyButton.nativeElement.classList.remove('Disactive');
      this.getKeyButton.nativeElement.classList.add('Active');
      this.getKeyButton.nativeElement.innerHTML = "Gửi mã";
      this.getKeyButton.nativeElement.disabled = false;
    }
  }
}
