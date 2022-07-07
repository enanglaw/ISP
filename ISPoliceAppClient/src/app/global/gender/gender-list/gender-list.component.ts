import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from 'src/environments/environment';

import { AbstractCreationDTO, AbstractUpdateDTO} from '../..//global.model';
import { GlobalService } from '../../global.service';


@Component({
  selector: 'app-gender-list',
  templateUrl: './gender-list.component.html',
  styleUrls: ['./gender-list.component.scss']
})
export class GenderListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  personnelData: MatTableDataSource<AbstractCreationDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'name'    
    
    /* 'actions', */
  ];

  constructor(private personnelService: GlobalService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.showProgressBar = true;
    this.personnelService.getGenderData().subscribe(
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
    this.personnelService.deleteGender(id).subscribe(() => {
      this.getAll();
    }); 
  }

  undelete(id: number) {
    /* this.personService.unDeletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }
}


