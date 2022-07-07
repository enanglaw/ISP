
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule, NgxMatNativeDateModule } from '@angular-material-components/datetime-picker';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { RichTextEditorAllModule } from '@syncfusion/ej2-angular-richtexteditor';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { MaterialModule } from 'src/app/material.module';
import { UtilitiesModule } from 'src/app/utilities/utilities.module';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import {MatPaginatorModule} from '@angular/material/paginator';



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
  RichTextEditorAllModule,
  MatAutocompleteModule,
  MatPaginatorModule
]

@NgModule({
  
  declarations: [
  ],//...COMPONENTS],
imports: [
  ...MODULES
],
exports:[
  ...MODULES
],

})
export class LeadersMasterModule { }
