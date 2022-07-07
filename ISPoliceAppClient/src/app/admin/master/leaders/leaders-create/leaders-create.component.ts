import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { LeadersList, OrganizationModelList, SubOrganizationModelList } from '../leaders.model';
import { LeadersService } from '../leaders.service';

@Component({
  selector: 'app-leaders-create',
  templateUrl: './leaders-create.component.html',
  styleUrls: ['./leaders-create.component.scss']
})
export class LeadersCreateComponent implements OnInit {

  @Input() model: LeadersList;
  @Input() organizationListmodel: LeadersList[]=[];
  @Input() organizationList: OrganizationModelList[]=[];
  @Input() subOrganizationListmodel: SubOrganizationModelList[]=[];
  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  count: number=0;
  progress = 0;
  heading: string = "New Leader";
  constructor(private formBuilder: FormBuilder,
    private leadersService: LeadersService,
    private dialog: MatDialog, private router: Router,
    private ref: ChangeDetectorRef)
  {

  }
 
  ngOnInit(): void {
    this.GetAllOrganization();
    this.inItForm();
 
  }

  onOrgSelected(event)
  {
    
     this.showLoading = true;
     this.GetOrganizationAndSubOrganizationList(event.value);     
    
  }
 
      
  GetOrganizationAndSubOrganizationList(Id) {
    let index = 0;
    this.subOrganizationListmodel = [];
    this.leadersService.getOrganizationDataById(Id).subscribe(    
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
        this.showLoading = false;
        console.log(error);
      }
    );
  }
  GetAllOrganization() {
    this.showLoading = true;
    this.leadersService.getOrganizationDropdown().subscribe(
    
      (data:any) => {  
               
        data.forEach(organization => {
          this.organizationListmodel.push(organization);                  
         })
        this.showLoading = false;
      },
      (error) => {
        this.showLoading = false;
        console.log(error);
      }
    );
  }
  inItForm() {
    this.form = this.formBuilder.group({
      id: 0,
      name: ['', { validators: [Validators.required] }],
      designation: ['', { validators: [Validators.required] }],
      address: [''],
      mobileNumber: [''], 
      subOrganizationId: [0],
      organizationId: [0],
      organizationName: [''],
      subOrganizationName: [''],
    
    });
    
  
    this.isFormLoded = true;
    

  }
 

  saveChanges() {
    if (this.form.valid) {      
     
      this.onSaveChanges(this.form.value)
    }
   
  }

 onSaveChanges(data: any) {
    console.log(data)
     this.leadersService.addLeaders(data).subscribe(a => {
      Swal.fire(
        'Success',
        'leader saved successfully',
        'success'
      ).then((result) => {
        this.router.navigate(['auth/leaders/list']);
      });
    },
    (error) => {
      (console.log(error))
      Swal.fire(
        'Error',
        'Some Error Occured',
        'error'
      ).then((result) => {
       // this.router.navigate(['/auth/leaders/list']);
      });
    })
  }

onCancel(){
  this.router.navigate(['/auth/leaders/list']);
}

}

