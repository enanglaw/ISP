import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileListRoutingModule } from './profile-list-routing.module';
import { ProfileMasterModule } from '../profile-master.module';
import { ProfileListComponent } from './profile-list.component';
import { TableModule } from 'primeng/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import {InputTextModule} from 'primeng/inputtext';

@NgModule({
  declarations: [ProfileListComponent],
  imports: [
    CommonModule,
    // BrowserModule,
    // BrowserAnimationsModule,
    ProfileListRoutingModule,
    ProfileMasterModule,
    TableModule,
    InputTextModule
  ]
})
export class ProfileListModule { }
