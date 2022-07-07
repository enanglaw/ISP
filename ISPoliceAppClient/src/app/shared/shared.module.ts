import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material.module';
import { FilterPipe } from './filter.pipe';

@NgModule({
  declarations: [FilterPipe],
  imports: [CommonModule, MaterialModule, FormsModule, ReactiveFormsModule],
  exports: [FilterPipe],
})
export class SharedModule {}
