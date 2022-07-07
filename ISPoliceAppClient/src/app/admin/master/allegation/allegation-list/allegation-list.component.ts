import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AllegationList } from '../allegation.model';
import { AllegationService } from '../allegation.service';

@Component({
  selector: 'app-allegation-list',
  templateUrl: './allegation-list.component.html',
  styleUrls: ['./allegation-list.component.scss']
})
export class AllegationListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  showLoading: boolean = false;  
  AllegationEventsData: MatTableDataSource<AllegationList>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'complainant',
    'accusedName',
    'accusedRank',
    'dateOfComplaint',
    'actions'
  ];
  constructor(private allegationService: AllegationService,private router:Router) {}

  ngOnInit(): void {
    this.getAllAllegations();
  }
      
 
  getAllAllegations() {
    this.showProgressBar = true;
    this.allegationService.getAllegations().subscribe(
      (data) => {
        this.AllegationEventsData = new MatTableDataSource(data);
        this.AllegationEventsData.paginator = this.paginator;
        this.resultsLength = this.AllegationEventsData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.AllegationEventsData = new MatTableDataSource();
        this.AllegationEventsData.paginator = this.paginator;
        this.resultsLength = this.AllegationEventsData.data.length;
        this.showProgressBar = false;
      }
    );
  }


  deleteAllegation(id: number) {   
    this.allegationService.deleteAllegation(id).subscribe(a => {
  Swal.fire(
    'Success',
    'Allegation deleted successfully',
    'success'
  ).then((result) => {
    this.getAllAllegations();
    
  });
},
(error) => {
  (console.log(error))
  Swal.fire(
    'Error',
    'Some Error Occured',
    'error'
  ).then((result) => {
    this.getAllAllegations();
  });
  })
 }
}
  



