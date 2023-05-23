import { ElementRef } from '@angular/core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  //styleUrls: ['./chatbot.component.css']
})
export class ChatbotComponent implements OnInit {

  @ViewChild('ChatboxInput', { static: false }) msgInput!: ElementRef;
  @ViewChild('ChatboxMain', { static: false }) msgChat!: ElementRef;
  
  dateNow: string = "";

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
  PERSON_NAME = "Thanh";

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    let currentDate = new Date();
    let day = String(currentDate.getDate()).padStart(2, '0');
    let month = String(currentDate.getMonth() + 1).padStart(2, '0');
    let year = String(currentDate.getFullYear());
    this.dateNow = `${day}/${month}/${year}`;
  }

  onSubmit() {

    if (this.msgInput == null)
    {
      return;
    }

    let msgText: string = this.msgInput.nativeElement.value;

    this.msgInput.nativeElement.value = "";

    if (msgText.trim() == '')
    {
      return;
    }

    this.appendMessage(this.PERSON_NAME, this.PERSON_IMG, "right", msgText);

    this.botResponse();

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

  botResponse(): void {
    let r: number = this.random(0, this.BOT_MSGS.length - 1);
    let msgText: string = this.BOT_MSGS[r];
    let delay: number = msgText.split(" ").length * 100;
    setTimeout(() => {
      this.appendMessage(this.BOT_NAME, this.BOT_IMG, "left", msgText);
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
