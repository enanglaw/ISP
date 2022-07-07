import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// region Syncfusion RTE
import { RichTextEditorAllModule } from '@syncfusion/ej2-angular-richtexteditor';
// endregion Syncfusion RTE
import { TableModule } from 'primeng/table';

import { CoreModule } from '../core/core.module';
import { MaterialModule } from '../material.module';
import { JsonFormComponent } from '../shared/json-form/json-form.component';
import { SharedModule } from '../shared/shared.module';
import { UtilitiesModule } from '../utilities/utilities.module';
import { PagesRouterModule } from './pages.routes';
import { CreatePersonComponent } from './person/create-person/create-person.component';
import { IndexPersonComponent } from './person/index-person/index-person.component';

@NgModule({
  imports: [
    CommonModule,
    PagesRouterModule,

    FlexLayoutModule,
    MaterialModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    UtilitiesModule,
    SharedModule,
    RichTextEditorAllModule,
    TableModule,
  ],
  declarations: [
    JsonFormComponent,
    CreatePersonComponent,
    IndexPersonComponent,
   
  ],
  exports: [
    JsonFormComponent,
    CreatePersonComponent,
    IndexPersonComponent,
    RichTextEditorAllModule,
    TableModule,
  ],
  providers: [],
  entryComponents: [
  ],
})
export class PagesModule {}
