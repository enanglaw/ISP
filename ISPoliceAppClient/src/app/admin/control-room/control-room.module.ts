import { NgModule } from '@angular/core';
import { FormControlRoomComponent } from './form-control-room/form-control-room.component';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule, NgxMatNativeDateModule } from '@angular-material-components/datetime-picker';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { RichTextEditorAllModule } from '@syncfusion/ej2-angular-richtexteditor';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { UtilitiesModule } from 'src/app/utilities/utilities.module';
import { MaterialModule } from 'src/app/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

const COMPONENTS=[FormControlRoomComponent]
const MODULES=[ 
  CommonModule,
  MaterialModule,
  FormsModule,
  ReactiveFormsModule,
  UtilitiesModule,
  FlexLayoutModule,
  MatDatepickerModule,
  NgxMatDatetimePickerModule,
  NgxMatTimepickerModule,
  NgxMatNativeDateModule ,
  NgxMaterialTimepickerModule,
  RichTextEditorAllModule]

@NgModule({
  declarations: [...COMPONENTS],
  imports: [
    ...MODULES
  ],exports:[
...MODULES,...COMPONENTS
  ]
})
export class ControlRoomModule { }
