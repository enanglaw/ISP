import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReligionCreateComponent } from './religion/religion-create/religion-create.component';
import { ReligionListComponent } from './religion/religion-list/religion-list.component';
import { ReligionEditComponent } from './religion/religion-edit/religion-edit.component';
import { GenderListComponent } from './gender/gender-list/gender-list.component';
import { GenderCreateComponent } from './gender/gender-create/gender-create.component';
import { GenderEditComponent } from './gender/gender-edit/gender-edit.component';
import { MaritalStatusEditComponent } from './marital-status/marital-status-edit/marital-status-edit.component';
import { MaritalStatusListComponent } from './marital-status/marital-status-list/marital-status-list.component';
import { MaritalStatusCreateComponent } from './marital-status/marital-status-create/marital-status-create.component';
import { RouterModule, Routes } from '@angular/router';
import { GenderFormComponent } from './gender/gender-form/gender-form.component';
import { ReligionFormComponent } from './religion/religion-form/religion-form.component';
import { MaritalStatusFormComponent } from './marital-status/marital-status-form/marital-status-form.component';
import { SubOrganizationFormComponent } from './sub-organization/sub-organization-form/sub-organization-form.component';
import { SubOrganizationListComponent } from './sub-organization/sub-organization-list/sub-organization-list.component';
import { SubOrganizationCreateComponent } from './sub-organization/sub-organization-create/sub-organization-create.component';
import { SubOrganizationEditComponent } from './sub-organization/sub-organization-edit/sub-organization-edit.component';
import { OrganizationMasterModule } from '../admin/master/organization/organization-master.module';

const routes: Routes = [
  { path: 'religion-list', component: ReligionListComponent },
  { path: 'religion-create', component: ReligionCreateComponent },
  {path:'religion-edit/:id',component: ReligionEditComponent},
  { path: 'gender-list', component: GenderListComponent },
  { path: 'gender-create', component: GenderCreateComponent },
  {path:'gender-edit/:id',component: GenderEditComponent},
  { path: 'marital-list', component: MaritalStatusListComponent },
  { path: 'marital-create', component: MaritalStatusCreateComponent },
  { path: 'marital-edit/:id', component: MaritalStatusEditComponent },
  { path: 'sub-organization-list', component: SubOrganizationListComponent },
  { path: 'sub-organization-create', component: SubOrganizationCreateComponent },
  { path: 'sub-organization-edit/:id', component: SubOrganizationEditComponent },
  
  
];

@NgModule({
  declarations: [
    ReligionCreateComponent,
    ReligionListComponent,
    ReligionEditComponent,
    GenderListComponent,
    GenderCreateComponent,
    GenderEditComponent,
    MaritalStatusEditComponent,
    MaritalStatusListComponent,
    MaritalStatusCreateComponent,
    GenderFormComponent,
    ReligionFormComponent,
    MaritalStatusFormComponent,
    SubOrganizationFormComponent,
    SubOrganizationListComponent,
    SubOrganizationCreateComponent,
    SubOrganizationEditComponent
  ],
  imports: [
    CommonModule,OrganizationMasterModule,
    RouterModule.forChild(routes)
  ]
})
export class GlobalModule { }
