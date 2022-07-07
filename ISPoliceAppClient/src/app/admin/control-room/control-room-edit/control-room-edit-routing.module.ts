import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ControlRoomEditComponent } from './control-room-edit.component';

const routes: Routes = [
  {path:'',component:ControlRoomEditComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ControlRoomEditRoutingModule { }
