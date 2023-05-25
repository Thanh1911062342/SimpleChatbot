import { ElementRef } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ChatBotService } from 'src/app/business/services/chatbotService';
import { StorageService } from 'src/app/business/services/storageService';

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  //styleUrls: ['./chatbot.component.css']
})
export class ChatbotComponent implements OnInit {

  @ViewChild('ChatboxInput', { static: false }) msgInput!: ElementRef;
  @ViewChild('ChatboxMain', { static: false }) msgChat!: ElementRef;
  
  dateNow: string = "";

  chatboxForm!: FormGroup;

  BOT_MSGS = [
    "Hi, how are you?",
    "Ohh... I can't understand what you trying to say. Sorry!",
    "I like to play games... But I don't know how to play!",
    "Sorry if my answers are not relevant. :))",
    "I feel sleepy! :("
  ];


  BOT_IMG = "https://image.flaticon.com/icons/svg/327/327779.svg";
  PERSON_IMG = "https://image.flaticon.com/icons/svg/145/145867.svg";
  BOT_NAME = "BOT";
  PERSON_NAME = "Me";

  isButtonDisabled: boolean = false;

  constructor(private chatbotService: ChatBotService,
              private storageService: StorageService) { }

  ngOnInit() {
    this.chatboxForm = new FormGroup({
      chatbox: new FormControl('', Validators.compose([
        Validators.required
      ])),
    });
  }

  ngAfterViewInit() {
    let currentDate = new Date();
    let day = String(currentDate.getDate()).padStart(2, '0');
    let month = String(currentDate.getMonth() + 1).padStart(2, '0');
    let year = String(currentDate.getFullYear());
    this.dateNow = `${day}/${month}/${year}`;
  }

  onSubmit() {

    if (this.chatboxForm.valid && this.isButtonDisabled == false) {

      this.isButtonDisabled = true;

      let msgText: string = this.chatboxForm.get('chatbox')?.value;

      this.chatboxForm.reset();

      this.appendMessage(this.PERSON_NAME, this.PERSON_IMG, "right", msgText);

      this.chatbotService.sendMessage(msgText).subscribe((response) => {
        if (response.body.isJWTValid == false) {
          alert(response.body.message);
          this.storageService.removeFromStorage('LoggedIn');

          return;
        }

        if (response.body.isSuccess == false) {
          alert(response.body.message);

          return;
        }

        this.botResponse(response.body.message);
      });
    }

  }

  appendMessage(name: string, img: string, side: string, text: string): void {
    const msgHTML: string = `
      <div class="Message" id="${side}Message">
        <div class="MessageImage" style="background-image: url(${img})"></div>
        <div class="MessageBubble">
          <div class="MessageInfo">
            <div class="MessageInfoName">${name}</div>
            <div class="MessageInfoTime">${this.formatDate(new Date())}</div>
          </div>
          <div class="MessageText">${text}</div>
        </div>
      </div>
    `;
    this.msgChat.nativeElement.insertAdjacentHTML("beforeend", msgHTML);
    this.msgChat.nativeElement.scrollTop += 500;
  }

  botResponse(message: string): void {
    let r: number = this.random(0, this.BOT_MSGS.length - 1);
    let msgText: string = message;
    let delay: number = 2000;
    setTimeout(() => {
      this.appendMessage(this.BOT_NAME, this.BOT_IMG, "left", msgText);
      this.isButtonDisabled = false;
    }, delay);
  }

  formatDate(date: Date): string {
    let day = String(date.getDate()).padStart(2, '0');
    let month = String(date.getMonth() + 1).padStart(2, '0');
    let year = String(date.getFullYear());
    let h: string = "0" + date.getHours();
    let m: string = "0" + date.getMinutes();
    return `${day}/${month}/${year} - ${h.slice(-2)}:${m.slice(-2)}`;
  }

  random(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min) + min);
  }
}
