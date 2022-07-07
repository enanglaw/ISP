import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileCreateComponent } from './profile-create.component';

const routes: Routes = [
  {path:'',component:ProfileCreateComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileCreateRoutingModule { }
