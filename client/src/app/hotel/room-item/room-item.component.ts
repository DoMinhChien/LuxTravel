import { Component, Input, OnInit } from '@angular/core';
import { IRoom } from 'src/app/shared/models/room';

@Component({
  selector: 'app-room-item',
  templateUrl: './room-item.component.html',
  styleUrls: ['./room-item.component.scss']
})
export class RoomItemComponent implements OnInit {
  @Input() room: IRoom;
  constructor() { }

  ngOnInit(): void {
  }

}
