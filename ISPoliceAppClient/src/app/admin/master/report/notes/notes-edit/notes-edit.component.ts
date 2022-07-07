import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { NotesList } from '../../report.model';
import { ReportService } from '../../report.service';

@Component({
  selector: 'app-notes-edit',
  templateUrl: './notes-edit.component.html',
  styleUrls: ['./notes-edit.component.scss']
})
export class NotesEditComponent implements OnInit {

 
  model: NotesList;
  notesModel: NotesList;
  errors: any;
  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private reportService: ReportService) {
    
    }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe((params) => {
      
      this.reportService.getNotesById(params.id)
        .subscribe((response) => {;
          this.model = response[0];
          this.notesModel=this.model[0]    
        
        });
    });
  }
  onCancel()
	{
	  this.router.navigate(['/auth/reports/notes-list']);
	}
  onSaveChanges(data: any) {
    
    this.reportService.editNotes(data.id, data)
      .subscribe(
        () => {
          Swal.fire(
            'Success',
            'Notes updated successfully',
            'success'
          ).then((result) => {
            this.router.navigate(['/auth/reports/notes-list']);
          });
        },
        (error) => {
          (console.log(error))
          Swal.fire(
            'Error',
            'Some Error Occured',
            'error'
          ).then((result) => {
            this.router.navigate(['/auth/report/notes-list']);
          });
        });
  }
}
