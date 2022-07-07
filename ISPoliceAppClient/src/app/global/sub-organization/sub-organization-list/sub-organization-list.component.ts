import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { GlobalService } from '../../global.service';
import {SubOrganizationDTO } from '../../global.model';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-sub-organization-list',
  templateUrl: './sub-organization-list.component.html',
  styleUrls: ['./sub-organization-list.component.scss']
})
export class SubOrganizationListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  subOrganizationData: MatTableDataSource<SubOrganizationDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'subOrganizationName',
    'organizationName',
    'description',
    'actions'
  ];


  constructor(private subOrganizationService: GlobalService,private router:Router) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.showProgressBar = true;
    this.subOrganizationService.getSubOrganizationData().subscribe(
      (data) => {
        this.subOrganizationData = new MatTableDataSource(data);
        this.subOrganizationData.paginator = this.paginator;
        this.resultsLength = this.subOrganizationData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.subOrganizationData = new MatTableDataSource();
        this.subOrganizationData.paginator = this.paginator;
        this.resultsLength = this.subOrganizationData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  showPersonDetail(id: number) {}

  delete(id: number) {   
    this.subOrganizationService.deleteSubOrganization(id).subscribe(a => {
  Swal.fire(
    'Success',
    'Sub Organization deleted successfully',
    'success'
  ).then((result) => {
    this.getAll();
   // this.router.navigate(['/auth/global/sub-organization-list']);
  });
},
(error) => {
  (console.log(error))
  Swal.fire(
    'Error',
    'Some Error Occured',
    'error'
  ).then((result) => {
    this.getAll();
    //this.router.navigate(['/auth/global/sub-organization-list']);
  });
})
}
  undelete(id: number) {
    /* this.subOrganizationService.unDeletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }
}



