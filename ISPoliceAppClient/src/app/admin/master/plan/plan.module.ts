import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlanUploadComponent } from './plan-upload/plan-upload.component';
import { PlanDownloadComponent } from './plan-download/plan-download.component';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationMasterModule } from '../organization/organization-master.module';
import { ProfileMasterModule } from '../profile/profile-master.module';
import { PlanFormComponent } from './plan-form/plan-form.component';

const routes: Routes = [

  { path: 'download', component: PlanDownloadComponent },
  { path: 'download/:id', component: PlanDownloadComponent },
  {path:'upload',component:PlanUploadComponent}
]

@NgModule({
  declarations: [
    PlanUploadComponent,
    PlanDownloadComponent,
    PlanFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    OrganizationMasterModule,
    ProfileMasterModule
  ]
})
export class PlanModule { }
