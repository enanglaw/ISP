import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { PersonGridViewDTO } from '../person.model';
import { PersonService } from '../person.service';

@Component({
  selector: 'app-index-person',
  templateUrl: './index-person.component.html',
  styleUrls: ['./index-person.component.scss'],
})
export class IndexPersonComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  personData: MatTableDataSource<PersonGridViewDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'personPhoto',
    'personName',
    'aliasNames',
    'parent',
    'gang',
    'status',
    'modusOperandi',
    /* 'actions', */
  ];
  constructor(private personService: PersonService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.showProgressBar = true;
    this.personService.getPersonGridData().subscribe(
      (data) => {
        this.personData = new MatTableDataSource(data);
        this.personData.paginator = this.paginator;
        this.resultsLength = this.personData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.personData = new MatTableDataSource();
        this.personData.paginator = this.paginator;
        this.resultsLength = this.personData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  showPersonDetail(id: number) {}

  delete(id: number) {
    /* this.personService.deletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }

  undelete(id: number) {
    /* this.personService.unDeletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }
}
