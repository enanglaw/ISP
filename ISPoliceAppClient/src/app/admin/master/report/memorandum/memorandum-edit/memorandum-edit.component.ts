import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { MemorandumList } from '../../report.model';
import { ReportService } from '../../report.service';

@Component({
  selector: 'app-memorandum-edit',
  templateUrl: './memorandum-edit.component.html',
  styleUrls: ['./memorandum-edit.component.scss']
})
export class MemorandumEditComponent implements OnInit {

  model: MemorandumList;
  memorandumModel: MemorandumList;
  errors: any;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private reportService: ReportService) {
    
    }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe((params) => {
      
      this.reportService
        .getMemorandumById(params.id)
        .subscribe((response) => {;
          this.model = response[0];
          this.memorandumModel=this.model[0]    
        
        });
    });
  }
  onCancel()
	{
	  this.router.navigate(['/auth/reports/memorandum-list']);
	}
  onSaveChanges(data: any) {
    
    this.reportService.editMemorandum(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Memorandum updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/report/memorandum-list']);
          });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            
          });
        });
  }
}
