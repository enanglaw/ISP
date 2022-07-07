import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotesListComponent } from './notes/notes-list/notes-list.component';
import { NotesCreateComponent } from './notes/notes-create/notes-create.component';
import { NotesFormComponent } from './notes/notes-form/notes-form.component';
import { NotesEditComponent } from './notes/notes-edit/notes-edit.component';
import { MemorandumEditComponent } from './memorandum/memorandum-edit/memorandum-edit.component';
import { MemorandumCreateComponent } from './memorandum/memorandum-create/memorandum-create.component';
import { MemorandumFormComponent } from './memorandum/memorandum-form/memorandum-form.component';
import { MemorandumListComponent } from './memorandum/memorandum-list/memorandum-list.component';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationMasterModule } from '../organization/organization-master.module';

const routes: Routes = [
  { path: "notes-list", component: NotesListComponent },
  { path: "notes-create", component:NotesCreateComponent},
  { path: "notes-edit/:id", component: NotesEditComponent },
  { path: "memorandum-list", component: MemorandumListComponent },
  { path: "memorandum-create", component:MemorandumCreateComponent},
  { path: "memorandum-edit/:id", component: MemorandumEditComponent },

]
 const MODULES = [
  RouterModule.forChild(routes),
  CommonModule, OrganizationMasterModule,
  
];

@NgModule({
  declarations: [
    NotesListComponent,
    NotesCreateComponent,
    NotesFormComponent,
    NotesEditComponent,
    MemorandumEditComponent,
    MemorandumCreateComponent,
    MemorandumFormComponent,
    MemorandumListComponent
  ],
  imports: [
    ... MODULES
  ]
})
export class ReportModule { }
