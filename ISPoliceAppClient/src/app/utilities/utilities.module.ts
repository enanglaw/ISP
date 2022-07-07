import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormControlRoomComponent } from '../admin/control-room/form-control-room/form-control-room.component';
import { DisplayErrorsComponent } from './display-errors/display-errors.component';
import { GenericListComponent } from './generic-list/generic-list.component';
import { InputImgComponent } from './input-img/input-img.component';
import { ReplaceSpaceToNbsp } from './replace-space-to-nbsp.pipe';

@NgModule({
  declarations: [
    DisplayErrorsComponent,
    GenericListComponent,
    InputImgComponent,
    ReplaceSpaceToNbsp,
    
  ],
  imports: [CommonModule],
  exports: [DisplayErrorsComponent, GenericListComponent, InputImgComponent, ReplaceSpaceToNbsp],
})
export class UtilitiesModule {}
