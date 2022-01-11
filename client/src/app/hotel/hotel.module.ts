import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HotelComponent } from './hotel.component';
import { SharedModule } from '../shared/shared.module';
import { HotelRoutingModule } from './hotel-routing.module';
import { HotelItemComponent } from './hotel-item/hotel-item.component';
import { HotelInfoComponent } from './hotel-info/hotel-info.component';
import { RoomItemComponent } from './room-item/room-item.component';
import { RoomInfoComponent } from './room-info/room-info.component';



@NgModule({
  declarations: [HotelComponent, HotelItemComponent, HotelInfoComponent, RoomItemComponent, RoomInfoComponent],
  imports: [
    CommonModule,
    SharedModule,
    HotelRoutingModule
  ]
})
export class HotelModule { }
