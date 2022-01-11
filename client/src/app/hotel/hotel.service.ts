import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HotelQueryParams } from '../home/hote.model';
import { IHotel } from '../shared/models/hotel';
import { IHotelDetail } from '../shared/models/hotel.detail';
import { IItemSelection } from '../shared/models/ItemSelection';
import { IPagination } from '../shared/models/pagination';
@Injectable({
  providedIn: 'root'
})
export class HotelService {
  baseUrl ='https://localhost:5002/api/';
  constructor(private http: HttpClient) { }


  getCities(){
    return this.http.get<IItemSelection[]>(this.baseUrl + 'master-data/cities');
  }

  getHotels(queryParams: HotelQueryParams)
  {
    let params = new HttpParams();
    let url = this.baseUrl + 'hotels';
    if(queryParams.selectedCityId){
      url = url + '?CityId= ' + queryParams.selectedCityId.toString();
      //params = params.append('CityId', queryParams.selectedCityId.toString());
    }
    if(queryParams.guestSelected){
      url = url + '&GuestCount= ' + queryParams.guestSelected.toString();

      //params = params.append('GuestCount', queryParams.guestSelected.toString());
    }
    // if (shopParams.search) {
    //   params = params.append('search', shopParams.search);
    // }
    //params = params.append('sort', this.shopParams.sort);
    url = url + '&PageIndex= ' + queryParams.pageNumber.toString();
    url = url + '&PageSize= ' + queryParams.pageSize.toString();

    // params = params.append('&PageIndex', queryParams.pageNumber.toString());
    // params = params.append('&PageSize', queryParams.pageSize.toString());
    return this.http.get<IHotel[]>(url);

  }

  getHotelDetail(id: string){
    return this.http.get<IHotelDetail>(this.baseUrl + 'hotels/'+id);

  }


  getRoomTypes(){
    return this.http.get<IItemSelection[]>(this.baseUrl + 'master-data/room-types');
  }
}
