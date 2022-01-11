import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HotelComponent } from './hotel.component';
import { HotelInfoComponent } from './hotel-info/hotel-info.component';


const routes : Routes = [
  {path: '', component: HotelComponent},
  {path: ':id', component: HotelInfoComponent, data:{breadcrumb: {alias: 'productDetails'}}},

]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class HotelRoutingModule { }
