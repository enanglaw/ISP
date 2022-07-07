import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileCreateRoutingModule } from './profile-create-routing.module';
import { ProfileCreateComponent } from './profile-create.component';
import { ProfileMasterModule } from '../profile-master.module';


@NgModule({
  declarations: [ProfileCreateComponent],
  imports: [
    CommonModule,   
    ProfileCreateRoutingModule,
    ProfileMasterModule
  ]
})
export class ProfileCreateModule { }
