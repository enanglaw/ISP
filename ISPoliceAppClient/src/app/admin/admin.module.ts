import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material.module';
import { UtilitiesModule } from '../utilities/utilities.module';
import { AdminRouterModule } from './admin-router.module';
import { CreateVenuePermissiontypeComponent } from './master/venue-permissiontype/create-venue-permissiontype/create-venue-permissiontype.component';
import { CreateVenueComponent } from './master/venue/create-venue/create-venue.component';
import { EditVenuePermissiontypeComponent } from './master/venue-permissiontype/edit-venue-permissiontype/edit-venue-permissiontype.component';
import { EditVenueComponent } from './master/venue/edit-venue/edit-venue.component';
import { FormVenuePermissiontypeComponent } from './master/venue-permissiontype/form-venue-permissiontype/form-venue-permissiontype.component';
import { FormVenueComponent } from './master/venue/form-venue/form-venue.component';
import { IndexVenuePermissiontypeComponent } from './master/venue-permissiontype/index-venue-permissiontype/index-venue-permissiontype.component';
import { IndexVenueComponent } from './master/venue/index-venue/index-venue.component';
import { CreateWorkflowComponent } from './workflow/create-workflow/create-workflow.component';
import { EditWorkflowComponent } from './workflow/edit-workflow/edit-workflow.component';
import { FormWorkflowComponent } from './workflow/form-workflow/form-workflow.component';
import { IndexWorkflowComponent } from './workflow/index-workflow/index-workflow.component';

let masterComponents = [
  IndexWorkflowComponent,
  CreateWorkflowComponent,
  EditWorkflowComponent,
  FormWorkflowComponent,
  IndexVenueComponent,
  CreateVenueComponent,
  EditVenueComponent,
  FormVenueComponent,
  FormVenuePermissiontypeComponent,
  CreateVenuePermissiontypeComponent,
  EditVenuePermissiontypeComponent,
  IndexVenuePermissiontypeComponent,
];
@NgModule({
  declarations: [...masterComponents],
  imports: [
    CommonModule,
    AdminRouterModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    UtilitiesModule
  ],
  exports: [...masterComponents],
})
export class AdminModule {}
