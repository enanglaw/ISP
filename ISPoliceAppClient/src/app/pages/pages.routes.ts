import { IndexPersonComponent } from './person/index-person/index-person.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreatePersonComponent } from './person/create-person/create-person.component';
/* import { ContactComponent } from './contact/contact.component';
import { AboutComponent } from './about/about.component';
import { ServicesComponent } from './services/services.component'; */

const pagesRoutes: Routes = [
  {
    path: 'input-form',
    component: IndexPersonComponent,
    data: { animation: 'inputform' },
  },
  
  {
    path:'person',component:IndexPersonComponent
  }, { path: 'person-create', component: CreatePersonComponent },
  
];

@NgModule({
  imports: [RouterModule.forChild(pagesRoutes)],
  exports: [RouterModule],
})
export class PagesRouterModule {}
