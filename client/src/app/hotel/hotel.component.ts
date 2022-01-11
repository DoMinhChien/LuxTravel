
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { HotelQueryParams } from '../home/hote.model';
import { IHotel } from '../shared/models/hotel';
import { IItemSelection } from '../shared/models/ItemSelection';
import { HotelService } from './hotel.service';

@Component({
  selector: 'app-hotel',
  templateUrl: './hotel.component.html',
  styleUrls: ['./hotel.component.scss']
})
export class HotelComponent implements OnInit {
  @ViewChild('search') searchTerm : ElementRef

  cities: IItemSelection[];
  roomTypes: IItemSelection[];
  queryParams: HotelQueryParams = new  HotelQueryParams();
  totalCount = 0;
  hotels: IHotel[];
  sortOptions = [
    { name: 'Alphabetical', id: 'name' },
    { name: 'Price: Low to High', id: 'priceAsc' },
    { name: 'Price: High to Low', id: 'priceDesc' }
  ];
  
  constructor(private hotelService : HotelService) { }

  ngOnInit(): void {
    let currentFilter = localStorage.getItem('current_filter');
    this.queryParams = JSON.parse(currentFilter);
    this.getProducts();
    
    this.getCities();
    this.getRoomTypes();
  }

  
  getRoomTypes(){
    this.hotelService.getRoomTypes().subscribe(response => 
      {
        this.roomTypes= response;
      });
  }
  getCities(){
    this.hotelService.getCities().subscribe(response => 
      {
        this.cities= response;
      });
  }
    
  getProducts(){
    this.hotelService.getHotels(this.queryParams).subscribe(response =>{
      this.hotels = response;
      // this.hotels =response.data;
      // this.queryParams.pageNumber = response.pageIndex;
      // this.queryParams.pageSize = response.pageSize;
      // this.totalCount = response.count;
    })
  }
  onSearch(){
    // this.shopParams.search = this.searchTerm.nativeElement.value;
    // this.shopParams.pageNumber =1;
    // this.getProducts();
  }
  
  onReset(){
    // this.searchTerm.nativeElement.value = '';
    // this.shopParams = new ShopParams();
    // this.getProducts();
  }
  
  onSortSelected(sort: string){
    // this.shopParams.sort = sort;
    // this.getProducts();
  }
  onCitySelected(id: string){
    debugger;
  }
}
