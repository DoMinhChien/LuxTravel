import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';

import { IItemSelection } from '../../models/ItemSelection';
import { City } from './drop-down.model';
@Component({
  selector: 'drop-down-component',
  templateUrl: './drop-down.component.html',
  styleUrls: ['./drop-down.component.scss']
})
export class DropDownComponent implements OnInit {
  @Input() options: IItemSelection[];

  @Input() selectedValue: any;
  @Output() selectedValueChange = new EventEmitter<any>();

  @Output() callBack = new EventEmitter<any>();
  selected: string;
  constructor() {

   }

  ngOnInit(): void {
  }

  onChange(event: any){
    this.selectedValueChange.emit(this.selected);
    this.callBack.emit();
  }
}
