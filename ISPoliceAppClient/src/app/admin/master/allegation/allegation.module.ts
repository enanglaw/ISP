import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AllegationFormComponent } from './allegation-form/allegation-form.component';
import { AllegationEditComponent } from './allegation-edit/allegation-edit.component';
import { AllegationListComponent } from './allegation-list/allegation-list.component';
import { AllegationCreateComponent } from './allegation-create/allegation-create.component';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationMasterModule } from '../organization/organization-master.module';

const routes: Routes = [
  { path: "allegation-list", component: AllegationListComponent },
  { path: "allegation-create", component:AllegationCreateComponent},
  { path: "allegation-edit/:id", component: AllegationEditComponent },
 ]
 const MODULES = [
  RouterModule.forChild(routes),
  CommonModule, OrganizationMasterModule,
  
];
@NgModule({
  declarations: [
    AllegationFormComponent,
    AllegationEditComponent,
    AllegationListComponent,
    AllegationCreateComponent
  ],
  imports: [
    ... MODULES
  ]
})
export class AllegationModule { }
