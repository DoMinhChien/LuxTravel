import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HotelService } from '../hotel/hotel.service';
import { IItemSelection } from '../shared/models/ItemSelection';
import { HotelQueryParams } from './hote.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  GuestOptions : IItemSelection[];

  RoomOptions:  IItemSelection[];
  
  queryParams: HotelQueryParams = new  HotelQueryParams();
  CityOptions: IItemSelection[];
  constructor(private hotelService : HotelService,public  router: Router) { 
    this.GuestOptions = [
      {id: 2, name: '2 adults'},
      {id: 3, name: '3 adults'}
    ]

    this.RoomOptions = [
      {id: 1, name: '1 room'},
      {id: 2, name: '2 rooms'}
    ];

  }

  ngOnInit(): void {
    this.queryParams.selectedCheckIn = new Date();
    this.queryParams.selectedCheckOut = new Date();
    this.getCities();
  }
  getCities(){
    this.hotelService.getCities().subscribe(response => 
      {
        this.CityOptions= response;
        this.queryParams.selectedCityId = 'CEB7A3D7-BEB0-4F97-9601-80C23E949FC7';
        this.queryParams.guestSelected = 1;
      });
  }

  getAvailableHotels()
  {
    localStorage.setItem('current_filter', JSON.stringify(this.queryParams));
    this.router.navigateByUrl(`hotels`);
  }
}
