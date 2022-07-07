import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { MemorandumList } from '../../report.model';
import { ReportService } from '../../report.service';

@Component({
  selector: 'app-memorandum-list',
  templateUrl: './memorandum-list.component.html',
  styleUrls: ['./memorandum-list.component.scss']
})
export class MemorandumListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  showLoading: boolean = false;  
  memorandumData: MatTableDataSource<MemorandumList>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'Title',
    'CreatedDate',
    'actions'
  ];
  constructor(private memorandumListService: ReportService,private router:Router) {}

  ngOnInit(): void {
    this.getMemorandumList();
  }
      
 
  getMemorandumList() {
    this.showProgressBar = true;
    this.memorandumListService.getMemorandumList().subscribe(
      (data) => {
        this.memorandumData = new MatTableDataSource(data);
        this.memorandumData.paginator = this.paginator;
        this.resultsLength = this.memorandumData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.memorandumData = new MatTableDataSource();
        this.memorandumData.paginator = this.paginator;
        this.resultsLength = this.memorandumData.data.length;
        this.showProgressBar = false;
      }
    );
  }


  deleteMemorandum(id: number) {   
    this.memorandumListService.deleteMemorandum(id).subscribe(a => {
  Swal.fire(
    'Success',
    'memorandum deleted successfully',
    'success'
  ).then((result) => {
    this.getMemorandumList();
    
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
  



