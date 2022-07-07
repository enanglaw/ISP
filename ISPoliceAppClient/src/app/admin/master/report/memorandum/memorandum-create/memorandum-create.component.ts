import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ReportService } from '../../report.service';

@Component({
  selector: 'app-memorandum-create',
  templateUrl: './memorandum-create.component.html',
  styleUrls: ['./memorandum-create.component.scss']
})
export class MemorandumCreateComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private reportService: ReportService) {
    
    }

    ngOnInit(): void {
    }
    onSaveChanges(data: any) {
    
      this.reportService.addMemorandum(data).subscribe(a => {
       Swal.fire(
         'Success',
         'Memorandum saved successfully',
         'success'
       ).then((result) => {
         this.router.navigate(['auth/reports/memorandum-list']);
       });
     },
     (error) => {
       (console.log(error))
       Swal.fire(
         'Error',
         'Some Error Occured',
         'error'
       ).then((result) => {
        this.router.navigate(['auth/reports/memorandum-list']);
      
       });
     })
   }
  }