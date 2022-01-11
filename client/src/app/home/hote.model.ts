export class HotelQueryParams{
    guestSelected: number;
    roomQuantitySelected: number;
    selectedCheckIn: Date;
    selectedCheckOut: Date;
    selectedCityId: string;
    sortSelected: string;
    pageNumber = 1;
    pageSize = 6;
    selectedRoomTypeId: string;
}