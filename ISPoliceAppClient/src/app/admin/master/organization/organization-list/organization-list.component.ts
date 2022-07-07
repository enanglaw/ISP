import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import Swal from 'sweetalert2';
import { OrganizationDTO } from '../organization.model';
import { OrganizationService } from '../organization.service';


@Component({
  selector: 'app-organization-list',
  templateUrl: './organization-list.component.html',
  styleUrls: ['./organization-list.component.scss'],
  encapsulation:ViewEncapsulation.None
})
export class OrganizationListComponent implements OnInit {
  serverUrl=environment.ServerUrl;
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  organizationData: MatTableDataSource<OrganizationDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  columnsToDisplay: string[] = ['SlNo', 'fullName', 'shortName', 'flagUrl', 'symbolUrl', 'actions'];
  

  constructor(private organizationService: OrganizationService,private router:Router) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.showProgressBar = true;
    this.organizationService.getOrganizationeGrid().subscribe(
      (data) => {
        this.organizationData = new MatTableDataSource(data);
        this.organizationData.paginator = this.paginator;
        this.resultsLength = this.organizationData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.organizationData = new MatTableDataSource();
        this.organizationData.paginator = this.paginator;
        this.resultsLength = this.organizationData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  showPersonDetail(id: number) {}

  delete(id: number) {   
    this.organizationService.deleteOrganization(id).subscribe(a => {
  Swal.fire(
    'Success',
    'Organization deleted successfully',
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

  }
}



