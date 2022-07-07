import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from 'src/environments/environment';

import { AbstractCreationDTO, AbstractUpdateDTO} from '../..//global.model';
import { GlobalService } from '../../global.service';


@Component({
  selector: 'app-marital-status-list',
  templateUrl: './marital-status-list.component.html',
  styleUrls: ['./marital-status-list.component.scss']
})
export class MaritalStatusListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  maritalStatusData: MatTableDataSource<AbstractCreationDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'name'    
    
    /* 'actions', */
  ];

  constructor(private maritalService: GlobalService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.showProgressBar = true;
    this.maritalService.getMaritalStatusData().subscribe(
      (data) => {
        this.maritalStatusData = new MatTableDataSource(data);
        this.maritalStatusData.paginator = this.paginator;
        this.resultsLength = this.maritalStatusData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.maritalStatusData = new MatTableDataSource();
        this.maritalStatusData.paginator = this.paginator;
        this.resultsLength = this.maritalStatusData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  showPersonDetail(id: number) {}

  delete(id: number) {
    this.maritalService.deleteMaritalStatus(id).subscribe(() => {
      this.getAll();
    }); 
  }

  undelete(id: number) {
    /* this.personService.unDeletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }
}
