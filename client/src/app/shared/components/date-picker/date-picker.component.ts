import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {FormControl} from '@angular/forms';
@Component({
  selector: 'date-picker-component',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent implements OnInit {
  startDate = new Date();
  

  minDate: Date;
  maxDate: Date;
  @Input() selectedDate: any;
  @Output() selectedDateChange = new EventEmitter<any>();
  constructor() {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date();
    this.maxDate = new Date(currentYear + 1, 11, 31);

   }

  ngOnInit(): void {
  } 

  onDateChange(event: any){
    this.selectedDateChange.emit(event.value);
    debugger;
  }
}
