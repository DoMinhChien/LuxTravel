import { Component, Input, OnInit } from '@angular/core';
import { IHotel } from 'src/app/shared/models/hotel';

@Component({
  selector: 'app-hotel-item',
  templateUrl: './hotel-item.component.html',
  styleUrls: ['./hotel-item.component.scss']
})
export class HotelItemComponent implements OnInit {
  @Input() product: IHotel;

  constructor() { }

  ngOnInit(): void {
  }

}
