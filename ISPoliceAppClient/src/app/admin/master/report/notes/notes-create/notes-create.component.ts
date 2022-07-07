import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ReportService } from '../../report.service';

@Component({
  selector: 'app-notes-create',
  templateUrl: './notes-create.component.html',
  styleUrls: ['./notes-create.component.scss']
})
export class NotesCreateComponent implements OnInit {
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private reportService: ReportService) {
    
    }

    ngOnInit(): void {
    }
    onSaveChanges(data: any) {
    
      this.reportService.addNotes(data).subscribe(a => {
       Swal.fire(
         'Success',
         'notes saved successfully',
         'success'
       ).then((result) => {
         this.router.navigate(['auth/reports/notes-list']);
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
     })
   }
  }