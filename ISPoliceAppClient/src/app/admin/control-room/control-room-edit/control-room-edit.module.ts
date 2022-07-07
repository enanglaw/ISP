import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ControlRoomEditRoutingModule } from './control-room-edit-routing.module';
import { ControlRoomEditComponent } from './control-room-edit.component';
import { ControlRoomModule } from '../control-room.module';
import { BrowserModule } from '@angular/platform-browser';


@NgModule({
  declarations: [ControlRoomEditComponent],
  imports: [
    CommonModule,
    ControlRoomEditRoutingModule,
    ControlRoomModule,
    
  ]
})
export class ControlRoomEditModule { }
