import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrganizationCreateComponent } from './organization-create/organization-create.component';
import { OrganizationListComponent } from './organization-list/organization-list.component';
import { OrganizationFormComponent } from './organization-form/organization-form.component';
import { OrganizationEditComponent } from './organization-edit/organization-edit.component';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationMasterModule } from './organization-master.module';
import { ProfileMasterModule } from '../profile/profile-master.module';
import { EventsEditComponent } from './events-edit/events-edit.component';
import { EventsFormComponent } from './events-form/events-form.component';
import { EventsCreateComponent } from './events-create/events-create.component';
import { EventsListComponent } from './events-list/events-list.component';
import { EventsComponent } from './events/events.component';




const routes: Routes = [
  { path: "org-list", component: OrganizationListComponent },
  { path: "org-create", component: OrganizationCreateComponent },
  { path: "org-edit/:id", component: OrganizationEditComponent },
  { path: "events-create", component: EventsCreateComponent },
  { path: "events-list", component: EventsListComponent },
  { path: "events-edit/:id", component: EventsEditComponent },
  
  { path: "events", component: EventsComponent },

]

const MODULES = [
  RouterModule.forChild(routes),
  CommonModule, OrganizationMasterModule,
  ProfileMasterModule 
];
@NgModule({
  
  declarations: [
    OrganizationCreateComponent,
    OrganizationListComponent,
    OrganizationFormComponent,
    OrganizationEditComponent,
    
    EventsListComponent,
    EventsEditComponent,
    EventsFormComponent,
    EventsCreateComponent,
    EventsComponent,
  ],//...COMPONENTS],
imports: [
  ...MODULES
  ],
 


})
export class OrganizationModule { }
