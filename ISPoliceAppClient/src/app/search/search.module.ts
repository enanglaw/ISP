import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MaterialModule } from '../material.module';
import { AdvancedSearchComponent } from './advanced-search.component';
import { SearchComponent } from './search.component';
import { UtilitiesModule } from '../utilities/utilities.module';

@NgModule({
  declarations: [SearchComponent, AdvancedSearchComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatButtonModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule,
    MaterialModule,
    UtilitiesModule,
  ],
})
export class SearchModule {}
