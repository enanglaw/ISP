import { ChangeDetectorRef, Component, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { EventDTO, OrganizationModelList, SubOrganizationModelList } from '../organization.model';
import { OrganizationService } from '../organization.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
  encapsulation:ViewEncapsulation.None
})
export class EventsComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  showLoading: boolean = false;
  form: FormGroup;
  @Input() organizationListmodel: OrganizationModelList[]=[];
  @Input() subOrganizationListmodel: SubOrganizationModelList[] = [];
  org: OrganizationModelList[] = [];
  OrganizationEventsData: MatTableDataSource<EventDTO>;
  SubOrganizationEventsData: MatTableDataSource<EventDTO>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'name',
    'title',
    'eventDate',
    'actions'
  ];


  constructor(private changeDetectorRefs: ChangeDetectorRef, private formBuilder: FormBuilder,private subOrganizationService: OrganizationService,private router:Router) {}

  ngOnInit(): void {
    this.getOrganizationEvents()
    this.GetAllOrganization();
    this.inItForm();
  }
  inItForm() {       
   this.form= this.formBuilder.group({
    
     eventsTransaction: this.formBuilder.group({
      organizationId: [0],
      subOrganizationId: [0],
     
    })
     
    })
    

  }
 
  onSubOrgSelected(event) {
    
    this.showProgressBar = true;
    if (event.value ==undefined) {
      this.getOrganizationEvents();
    }
    //this.GetSubOrganizationList(event.value);
    this.subOrganizationService.getSubOrganizationEventDataById(event.value).subscribe(
      (data)=>{
        console.log(data);
        this.OrganizationEventsData = new MatTableDataSource(data);
        this.changeDetectorRefs.detectChanges();
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
        this.showProgressBar = false;
       
      },
      (error) => {
        this.OrganizationEventsData = new MatTableDataSource();
        this.changeDetectorRefs.detectChanges();
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
        this.showProgressBar = false;
        
      }
    );

  }
   onOrgSelected(event)
  {
    
     this.showProgressBar = true;
     if (event.value ==undefined) {      
      this.getOrganizationEvents();
    }
     this.GetOrganizationList(event.value);     
     this.subOrganizationService.getOrganizationEventDataById(event.value).subscribe(
       (data) => {
         this.OrganizationEventsData = new MatTableDataSource(data);
         this.changeDetectorRefs.detectChanges();
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
         this.showProgressBar = false;
        
      },
      (error) => {
        console.log(error);
        this.OrganizationEventsData = new MatTableDataSource();
        this.changeDetectorRefs.detectChanges();
        this.OrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.OrganizationEventsData.data.length;
        this.showProgressBar = false;
    
      }
    );
  }

      
  GetOrganizationList(Id) {
    let index = 0;
    this.subOrganizationListmodel = [];
    this.subOrganizationService.getOrganizationById(Id).subscribe(    
      (data:any) =>
      { 
        let index = 0;
           data.forEach(result => {
         (result[index].subOrganizationCategory.forEach(
          subOrganization => {
            this.subOrganizationListmodel.push(subOrganization)           
            }
          ))
          
          index++;
        }) 
     
      },
      (error) =>
      {
        this.showProgressBar = false;
        console.log(error);
      }
    );
  }

  GetSubOrganizationList(Id) {
    let index = 0;
    this.subOrganizationListmodel = [];
    this.subOrganizationService.getOrganizationById(Id).subscribe(    
      (data:any) =>
      { 
        let index = 0;
           data.forEach(result => {
         (result[index].subOrganizationCategory.forEach(
          subOrganization => {
            this.subOrganizationListmodel.push(subOrganization)           
            }
          ))
          
          index++;
        }) 
     
      },
      (error) =>
      {
        this.showProgressBar = false;
        console.log(error);
      }
    );
  }
  GetAllOrganization() {
    this.showProgressBar = true;
    this.subOrganizationService.getOrganizationDropdown().subscribe(
    
      (data:any) => {  
               
        data.forEach(organization => {
          this.organizationListmodel.push(organization);                  
         })
        this.showProgressBar = false;
      },
      (error) => {
        this.showProgressBar = false;
        console.log(error);
      }
    );
  }
  getSubOrganizationEvents() {
    this.showProgressBar = true;
    this.subOrganizationService.getOrganizationEventData().subscribe(
      (data) => {
        this.SubOrganizationEventsData = new MatTableDataSource(data);
        this.SubOrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.SubOrganizationEventsData.data.length;
        this.showProgressBar = false;
      },
      (error) => {
        console.log(error);
        this.SubOrganizationEventsData = new MatTableDataSource();
        this.SubOrganizationEventsData.paginator = this.paginator;
        this.resultsLength = this.SubOrganizationEventsData.data.length;
        this.showProgressBar = false;
      }
    );
  }
  getOrganizationEvents() {
    this.showProgressBar = true;
    this.subOrganizationService.getOrganizationEventData().subscribe(
      (data) => {
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
    'Events deleted successfully',
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
    this.getSubOrganizationEvents();
    //this.router.navigate(['/auth/global/sub-organization-list']);
  });
  })
 }
}
  



