import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { NotesList } from '../../report.model';
import { ReportService } from '../../report.service';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrls: ['./notes-list.component.scss']
})
export class NotesListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  showLoading: boolean = false;  
  notesData: MatTableDataSource<NotesList>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'Title',
    'CreatedDate',
    'actions'
  ];
  constructor(private reportService: ReportService,private router:Router) {}

  ngOnInit(): void {
    this.getNotes();
  }
      
 
  getNotes() {
    this.showProgressBar = true;
    this.reportService.getNoteList().subscribe(
      (data) => {
        this.notesData = new MatTableDataSource(data);
        this.notesData.paginator = this.paginator;
        this.resultsLength = this.notesData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.notesData = new MatTableDataSource();
        this.notesData.paginator = this.paginator;
        this.resultsLength = this.notesData.data.length;
        this.showProgressBar = false;
      }
    );
  }


  deleteNotes(id: number) {   
    this.reportService.deleteNotes(id).subscribe(a => {
  Swal.fire(
    'Success',
    'Note deleted successfully',
    'success'
  ).then((result) => {
    this.getNotes();
    
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
  



