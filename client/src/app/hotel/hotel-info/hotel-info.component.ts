import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IHotelDetail } from 'src/app/shared/models/hotel.detail';
import { IRoom } from 'src/app/shared/models/room';
import { BreadcrumbService } from 'xng-breadcrumb';
import { HotelService } from '../hotel.service';

@Component({
  selector: 'app-hotel-info',
  templateUrl: './hotel-info.component.html',
  styleUrls: ['./hotel-info.component.scss']
})
export class HotelInfoComponent implements OnInit {
  hotel: IHotelDetail;
  rooms: IRoom[];
  constructor(private hotelService: HotelService,
    private activateRoute: ActivatedRoute,
    private bcService: BreadcrumbService) {
        this.bcService.set('@productDetails', ' ');
     }
  ngOnInit(): void {
    this.hotelService.getHotelDetail(this.activateRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.hotel = response;
      this.rooms = response.availableRooms;
      this.bcService.set('@productDetails', this.hotel.hotelName);
    });
  }

}
