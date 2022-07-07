import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { workflowCreationDTO, workflowDTO } from '../workflow.models';
import { WorkflowService } from '../workflow.service';

@Component({
  selector: 'app-edit-workflow',
  templateUrl: './edit-workflow.component.html',
  styleUrls: ['./edit-workflow.component.scss'],
})
export class EditWorkflowComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private workflowService: WorkflowService,
    private router: Router
  ) {}

  model: workflowDTO;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      console.log(params);
      this.workflowService.getById(params.id).subscribe((genre) => {
        this.model = genre;
      });
    });
  }

  saveChanges(workflowCreationDTO: workflowCreationDTO) {
    this.workflowService
      .edit(this.model.workflowId, workflowCreationDTO)
      .subscribe(() => {
        this.router.navigate(['/auth/workflow']);
      });
  }
}
