import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-alert-error',
  templateUrl: './alert-error.component.html',
  styleUrls: ['./alert-error.component.css']
})
export class ErrorAlertComponent implements OnInit {

  @Input()
  errorMessage: string = "Something went wrong";

  @Input()
  show: boolean;

  constructor() { }

  ngOnInit(): void {
  }

}
