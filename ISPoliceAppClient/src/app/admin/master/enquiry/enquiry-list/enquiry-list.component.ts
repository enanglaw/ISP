import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { EnquiryList } from '../enquiry.model';
import { EnquiryService } from '../enquiry.service';

@Component({
  selector: 'app-enquiry-list',
  templateUrl: './enquiry-list.component.html',
  styleUrls: ['./enquiry-list.component.scss']
})
export class EnquiryListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  showLoading: boolean = false;  
  EnquiryData: MatTableDataSource<EnquiryList>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'Title',
    'CreatedDate',
    'actions'
  ];
  constructor(private enquiryService: EnquiryService,private router:Router) {}

  ngOnInit(): void {
    this.getEnquiries();
  }
      
 
  getEnquiries() {
    this.showProgressBar = true;
    this.enquiryService.getAllegationList().subscribe(
      (data) => {
        this.EnquiryData = new MatTableDataSource(data);
        this.EnquiryData.paginator = this.paginator;
        this.resultsLength = this.EnquiryData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.EnquiryData = new MatTableDataSource();
        this.EnquiryData.paginator = this.paginator;
        this.resultsLength = this.EnquiryData.data.length;
        this.showProgressBar = false;
      }
    );
  }


  deleteEnquiry(id: number) {   
    this.enquiryService.deleteAllegationEnquiry(id).subscribe(a => {
  Swal.fire(
    'Success',
    'enquiry deleted successfully',
    'success'
  ).then((result) => {
    this.getEnquiries();
    
  });
},
(error) => {
  (console.log(error))
  Swal.fire(
    'Error',
    'Some Error Occured',
    'error'
  ).then((result) => {
    this.getEnquiries();
  });
  })
 }
}
  



