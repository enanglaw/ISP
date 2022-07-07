import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ControlRoomListRoutingModule } from './control-room-list-routing.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material.module';
import { UtilitiesModule } from 'src/app/utilities/utilities.module';
import { ControlRoomListComponent } from './control-room-list.component';



@NgModule({
  declarations: [
    ControlRoomListComponent
  ],
  imports: [
    CommonModule,
    ControlRoomListRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    UtilitiesModule,
    FlexLayoutModule,
    
  ]
})
export class ControlRoomListModule { }
