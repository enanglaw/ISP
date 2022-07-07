import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from 'src/environments/environment';

import { AbstractCreationDTO, AbstractUpdateDTO} from '../..//global.model';
import { GlobalService } from '../../global.service';


@Component({
  selector: 'app-religion-list',
  templateUrl: './religion-list.component.html',
  styleUrls: ['./religion-list.component.scss']
})
export class ReligionListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  religionData: MatTableDataSource<AbstractCreationDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'name'    
    
    /* 'actions', */
  ];

  constructor(private religionService: GlobalService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.showProgressBar = true;
    this.religionService.getReligionData().subscribe(
      (data) => {
        this.religionData = new MatTableDataSource(data);
        this.religionData.paginator = this.paginator;
        this.resultsLength = this.religionData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.religionData = new MatTableDataSource();
        this.religionData.paginator = this.paginator;
        this.resultsLength = this.religionData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  showPersonDetail(id: number) {}

  delete(id: number) {
    this.religionService.deleteGender(id).subscribe(() => {
      this.getAll();
    }); 
  }

  undelete(id: number) {
    /* this.religionService.unDeletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }
}