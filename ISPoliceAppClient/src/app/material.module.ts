import { DragDropModule } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
// import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { MatRadioModule } from '@angular/material/radio';

const materialModules = [
  CKEditorModule,
  MatExpansionModule,
  MatToolbarModule,
  MatButtonModule,
  MatCardModule,
  MatDialogModule,
  MatIconModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatCheckboxModule,
  MatDatepickerModule,
  MatListModule,
  MatRadioModule,
  MatNativeDateModule,
  MatTabsModule,
  MatAutocompleteModule,
  MatTableModule,
  DragDropModule,
  MatProgressSpinnerModule,
  MatProgressBarModule,
  MatPaginatorModule,
  MatChipsModule,
  MatDividerModule,
  MatChipsModule 
];
@NgModule({
  declarations: [],
  exports: [...materialModules, SweetAlert2Module],
  imports: [...materialModules, CommonModule, SweetAlert2Module.forRoot()],
})
export class MaterialModule {}
