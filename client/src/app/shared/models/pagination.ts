import { IHotel } from "./hotel";

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IHotel[];
}

export class Pagination implements IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IHotel[] = [];
}
 