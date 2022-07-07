import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ControlRoomCreateRoutingModule } from './control-room-create-routing.module';
import { ControlRoomCreateComponent } from './control-room-create.component';
import { ControlRoomModule } from '../control-room.module';
@NgModule({
  declarations: [ControlRoomCreateComponent],
  imports: [
    CommonModule,
    ControlRoomCreateRoutingModule,
    ControlRoomModule
  ]
})
export class ControlRoomCreateModule { }
