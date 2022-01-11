import { IRoom } from "./room";

export interface IHotelDetail {
    id: string;
    hotelName: string;
    guestId: string;
    dateFrom: Date;
    dateTo: Date;
    guestCount: number;
    statusId: string;
    imageUrls: string[];
    availableRooms: IRoom[];
    url: string;
    email: string;
    avgRating: number;
    reviewers: number;
    embedUrl: string;
    location: string;
}