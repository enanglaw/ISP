import { Component, OnInit } from '@angular/core';
import { workflowDTO } from '../workflow.models';
import { WorkflowService } from '../workflow.service';

@Component({
  selector: 'app-workflow',
  templateUrl: './index-workflow.component.html',
  styleUrls: ['./index-workflow.component.scss'],
})
export class IndexWorkflowComponent implements OnInit {
  workflowData: workflowDTO[] = [];
  columnsToDisplay: string[] = ['Name', /* 'Active',  */ 'actions'];
  constructor(private workflowService: WorkflowService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.workflowService.getAll().subscribe(
      (data) => {
        this.workflowData = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  delete(id: number) {
    this.workflowService.delete(id).subscribe(() => {
      this.getAll();
    });
  }
}
