import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PersonnelFormComponent } from './personnel-form/personnel-form.component';
import { PersonnelListComponent } from './personnel-list/personnel-list.component';
import { PersonnelCreateComponent } from './personnel-create/personnel-create.component';
import { OrganizationMasterModule } from '../organization/organization-master.module';
import { PersonnelEditComponent } from './personnel-edit/personnel-edit.component';


const routes: Routes = [
  {path:'personnel-list', component:PersonnelListComponent
  },
  { path: 'personnel-create', component: PersonnelCreateComponent },
  {path:'personnel-edit/:id',component:PersonnelEditComponent}
]

const MODULES = [
  RouterModule.forChild(routes),
  CommonModule, 
  OrganizationMasterModule 
];
@NgModule({
  
  declarations: [
  
    PersonnelCreateComponent,
    PersonnelEditComponent,
       PersonnelFormComponent,
       PersonnelListComponent
  ],//...COMPONENTS],
imports: [
  ...MODULES
  ],
 


})
export class PersonnelModule { }

