import { Component, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import Swal from 'sweetalert2';
import { EventDTO, OrganizationModelList, SubOrganizationDTO, SubOrganizationModelList } from '../organization.model';
import { OrganizationService } from '../organization.service';


@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.scss']
})
export class EventsListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  showLoading: boolean = false;
  OrganizationEventsData: MatTableDataSource<EventDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'name',
    'title',
    'eventDate',
    'actions'
  ];


  constructor(private subOrganizationService: OrganizationService,private router:Router) {}

  ngOnInit(): void {
    this.getOrganizationEvents();
    this.getSubOrganizationEvents() 

  }
  
 
  getSubOrganizationEvents() {
    this.showProgressBar = true;
 
    this.subOrganizationService.getSubOrganizationEventData().subscribe(
      (data) => {
        console.log(data)
        this.OrganizationEventsData = new MatTableDataSource(data);
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.OrganizationEventsData = new MatTableDataSource();
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  getOrganizationEvents() {
    this.showProgressBar = true;
 
    this.subOrganizationService.getOrganizationEventData().subscribe(
      (data) => {
        console.log(data)
        this.OrganizationEventsData = new MatTableDataSource(data);
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.OrganizationEventsData = new MatTableDataSource();
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
        this.showProgressBar = false;
      }
    );
  }

  showPersonDetail(id: number) {}

  deleteEvents(id: number) {
    this.subOrganizationService.deleteSubOrganization(id).subscribe(a => {
      Swal.fire(
        'Success',
        'Sub Organization events deleted successfully',
        'success'
      ).then((result) => {
        this.getOrganizationEvents();
       
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