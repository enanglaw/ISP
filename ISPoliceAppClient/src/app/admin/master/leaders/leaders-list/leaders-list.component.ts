import { ChangeDetectorRef, Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { LeaderDTO, LeadersDTO, LeadersModel, OrganizationModelList, SubOrganizationDTO, SubOrganizationModelList } from '../leaders.model';
import { LeadersService } from '../leaders.service';

@Component({
  selector: 'app-leaders-list',
  templateUrl: './leaders-list.component.html',
  styleUrls: ['./leaders-list.component.scss']
})
export class LeadersListComponent implements OnInit {
  showProgressBar: boolean;
  errors: string[] = [];
  resultsLength!: number;
  pageSize = 5;
  showLoading: boolean = false;
  form: FormGroup;
  @Input() organizationListmodel: OrganizationModelList[]=[];
  @Input() subOrganizationListmodel: SubOrganizationModelList[]=[];
  OrganizationLeadersData: MatTableDataSource<LeadersModel>;
  @ViewChild('paginator') paginator: MatPaginator;
  
  columnsToDisplay: string[] = [
    'SlNo',
    'groupName',
    'name',
    'designation', 
    'address',
    'actions'
  ];


  constructor(private changeDetectorRefs: ChangeDetectorRef,
    private formBuilder: FormBuilder,
    private leaderService: LeadersService,
    private router: Router) { }

  ngOnInit(): void {
   this.getAllLeaderShip();
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
  getAllLeaderShip() {

    this.showProgressBar = true;
    this.leaderService.getAllLeaderShip().subscribe(
      (data)=>{
        
        this.OrganizationLeadersData = new MatTableDataSource(data);
        this.OrganizationLeadersData.paginator = this.paginator;
        this.resultsLength = this.OrganizationLeadersData.data.length;
        this.showProgressBar = false;
       
      },
      (error) => {
        this.OrganizationLeadersData = new MatTableDataSource();
        this.OrganizationLeadersData.paginator = this.paginator;
        this.resultsLength = this.OrganizationLeadersData.data.length;
        this.showProgressBar = false;
        
      }
    );


  }
  onSubOrgSelected(event) {
    
    this.showProgressBar = true;
    this.leaderService.getSubOrganizationLeaderDataById(event.value).subscribe(
      (data)=>{
        
        this.OrganizationLeadersData = new MatTableDataSource(data);
        this.OrganizationLeadersData.paginator = this.paginator;
        this.resultsLength = this.OrganizationLeadersData.data.length;
        this.showProgressBar = false;
       
      },
      (error) => {
        this.OrganizationLeadersData = new MatTableDataSource();
        this.OrganizationLeadersData.paginator = this.paginator;
        this.resultsLength = this.OrganizationLeadersData.data.length;
        this.showProgressBar = false;
        
      }
    );

  }
   onOrgSelected(event)
  {
    
     this.showProgressBar = true;
     this.GetOrganizationAndSubOrganizationList(event.value); 
     this.leaderService.getOrganizationLeaderDataById(event.value).subscribe(
       (data) => {
         this.OrganizationLeadersData = new MatTableDataSource(data);
        this.OrganizationLeadersData.paginator = this.paginator;
        this.resultsLength = this.OrganizationLeadersData.data.length;
         this.showProgressBar = false;
        
      },
      (error) => {
        console.log(error);
        this.OrganizationLeadersData = new MatTableDataSource();
        this.OrganizationLeadersData.paginator = this.paginator;
        this.resultsLength = this.OrganizationLeadersData.data.length;
        this.showProgressBar = false;
    
      }
    );
  }

      
 
 GetOrganizationAndSubOrganizationList(Id) {
    let index = 0;
    this.subOrganizationListmodel = [];
    this.leaderService.getOrganizationDataById(Id).subscribe(    
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
    this.leaderService.getOrganizationDropdown().subscribe(
      (data) => {
        this.organizationListmodel = data;    
            this.showProgressBar = false;
      },
      (error) => {
        this.showProgressBar = false;
        console.log(error);
      }
    );
  }  
 

 
  showPersonDetail(id: number) {}

  deleteLeaders(id: number) {   
    this.leaderService.deleteLeaders(id).subscribe(a => {
  Swal.fire(
    'Success',
    'Leader deleted successfully',
    'success'
  ).then((result) => {
    this.getAllLeaderShip();
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
    /* this.subOrganizationService.unDeletePerson(id).subscribe(() => {
      this.getAll();
    }); */
  }
}



