import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LeadersMasterModule } from './leaders-master.module';
import { ProfileMasterModule } from '../profile/profile-master.module';
import { LeadersCreateComponent } from './leaders-create/leaders-create.component';
import { LeadersListComponent } from './leaders-list/leaders-list.component';
import { LeadersFormComponent } from './leaders-form/leaders-form.component';
import { LeadersEditComponent } from './leaders-edit/leaders-edit.component';




const routes: Routes = [
  {path:'list', component:LeadersListComponent
  },
  { path: 'create', component: LeadersCreateComponent },
  {path:'edit/:id',component:LeadersEditComponent}
]

const MODULES = [
  RouterModule.forChild(routes),
  CommonModule, LeadersMasterModule,
  ProfileMasterModule 
];
@NgModule({
  
  declarations: [
  
    LeadersCreateComponent,
       LeadersListComponent,
       LeadersFormComponent,
       LeadersEditComponent
  ],//...COMPONENTS],
imports: [
  ...MODULES
  ],
 


})
export class LeadersModule { }
