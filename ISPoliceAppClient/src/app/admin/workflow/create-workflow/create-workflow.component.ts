import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { parseWebAPIErrors } from 'src/app/utilities/utils';
import { workflowCreationDTO } from '../workflow.models';
import { WorkflowService } from '../workflow.service';

@Component({
  selector: 'app-create-workflow',
  templateUrl: './create-workflow.component.html',
  styleUrls: ['./create-workflow.component.scss'],
})
export class CreateWorkflowComponent implements OnInit {
  errors: string[] = [];
  constructor(
    private router: Router,
    private workflowService: WorkflowService
  ) {}

  ngOnInit(): void {}

  saveChanges(workflowCreationDTO: workflowCreationDTO) {
    this.workflowService.create(workflowCreationDTO).subscribe(
      () => {
        this.router.navigate(['/auth/workflow']);
      },
      (error) => {
        this.errors = parseWebAPIErrors(error);
      }
    );
  }
}
