import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ControlRoomCreateComponent } from './control-room-create.component';

const routes: Routes = [{path:'',component:ControlRoomCreateComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ControlRoomCreateRoutingModule { }
