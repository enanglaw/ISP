import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnquiryEditComponent } from './enquiry-edit/enquiry-edit.component';
import { EnquiryFormComponent } from './enquiry-form/enquiry-form.component';
import { EnquiryListComponent } from './enquiry-list/enquiry-list.component';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationMasterModule } from '../organization/organization-master.module';
import { EnquiryCreateComponent } from './enquiry-create/enquiry-create.component';

const routes: Routes = [
  { path: "enquiry-list", component: EnquiryListComponent },
  { path: "enquiry-create", component:EnquiryCreateComponent},
  { path: "enquiry-edit/:id", component: EnquiryEditComponent },
  ]
 const MODULES = [
  RouterModule.forChild(routes),
  CommonModule, OrganizationMasterModule,
  
];

@NgModule({
  declarations: [
    EnquiryEditComponent,
    EnquiryFormComponent,
    EnquiryListComponent,
    EnquiryCreateComponent,
  ],
  imports: [
  ... MODULES
  ]
})
export class EnquiryModule { }
