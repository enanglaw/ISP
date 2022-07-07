import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateWorkflowComponent } from './workflow/create-workflow/create-workflow.component';
import { EditWorkflowComponent } from './workflow/edit-workflow/edit-workflow.component';
import { IndexWorkflowComponent } from './workflow/index-workflow/index-workflow.component';

export const workflowRoutes: Routes = [
  { path: 'workflow', component: IndexWorkflowComponent },
  { path: 'workflow-create', component: CreateWorkflowComponent },
  { path: 'workflow-edit', component: EditWorkflowComponent },
];

@NgModule({
  imports: [RouterModule.forChild(workflowRoutes)],
  exports: [RouterModule],
})
export class AdminRouterModule {}
