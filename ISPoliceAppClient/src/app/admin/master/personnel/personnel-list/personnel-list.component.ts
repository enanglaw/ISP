import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from 'src/environments/environment';
import Swal from 'sweetalert2';
import { PersonnelList } from '../personnel.model';
import { PersonnelService } from '../personnel.service';

@Component({
  selector: 'app-personnel-list',  
  templateUrl: './personnel-list.component.html',
  styleUrls: ['./personnel-list.component.scss']
})
export class PersonnelListComponent implements OnInit {
  
  serverUrl=environment.ServerUrl;
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  personnelData: MatTableDataSource<PersonnelList>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'personnelPhotoUrl',    
    'name',
    'currentRank',
    'personalNumber',
    'email',
    'actions'

  ];

  constructor(private personnelService: PersonnelService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.showProgressBar = true;
    this.personnelService.getPersonnels().subscribe(
      (data) => {
        this.personnelData = new MatTableDataSource(data);
        this.personnelData.paginator = this.paginator;
        this.resultsLength = this.personnelData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.personnelData = new MatTableDataSource();
        this.personnelData.paginator = this.paginator;
        this.resultsLength = this.personnelData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  showPersonDetail(id: number) {}
  delete(id: number) {   
    this.personnelService.deletePersonnel(id).subscribe(a => {
  Swal.fire(
    'Success',
    'personnel deleted successfully',
    'success'
  ).then((result) => {
  
    this.getAll();
  });
},
(error) => {
  (console.log(error))
  Swal.fire(
    'Error',
    'Some Error Occured',
    'error'
  ).then((result) => {
    //this.router.navigate(['/auth/global/sub-organization-list']);
  });
})
  }
 

  undelete(id: number) {
    /* this.personService.unDeletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }
}


