import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileEditRoutingModule } from './profile-edit-routing.module';
import { ProfileMasterModule } from '../profile-master.module';
import { ProfileEditComponent } from './profile-edit.component';


@NgModule({
  declarations: [ProfileEditComponent],
  imports: [
    CommonModule,
    ProfileEditRoutingModule,
    ProfileMasterModule
  ]
})
export class ProfileEditModule { }
