import { HttpEventType } from '@angular/common/http';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { SubOrganizationDTO,OrganizationDTO, OrganizationList, SubOrganization, OrganizationProfileList, SubOrganizationDetails} from '../../global.model';
import { GlobalService } from '../../global.service'; 
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router'

@Component({
  selector: 'app-sub-organization-form',
  templateUrl: './sub-organization-form.component.html',
  styleUrls: ['./sub-organization-form.component.scss']
})
export class SubOrganizationFormComponent implements OnInit {

   @Input() model: SubOrganization;
   @Input() organizationListmodel: SubOrganization[]=[];

  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();

  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  progress = 0;
  // DATA Sources

  subOrganizationDataSource = new BehaviorSubject<AbstractControl[]>([]);
  subOrganizationDataSourceColumns: string[] = ['No', 'Name', 'Description', 'actions'];


  // END of DATA Sources

  constructor(private formBuilder: FormBuilder, private profileService: GlobalService,
    private dialog: MatDialog,private router: Router,private ref: ChangeDetectorRef) {

  }


  ngOnInit(): void {
    this.GetAllOrganization();
    this.inItForm();


  }

  get f() {
    return this.form.controls;
  }
 
  get subOrganizations() : FormArray {
    return this.form.get("subOrganizations") as FormArray
  }
 

  GetAllOrganization() {
    this.profileService.getOrganizationDropdown().subscribe(
      (data) => {
        this.organizationListmodel = data;
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
      fullName: [''], 
      organization:[''],
      organizationId: [0],             
      subOrganizations: new FormArray([

      ]),

      
    });
    
    if (this.model !== undefined) {
      this.form.patchValue(this.model);
	  let _subOrganizations = (this.form.get('subOrganizations') as FormArray);
      _subOrganizations.clear();
      
        _subOrganizations.push(this.newSubOrganizations(this.model))
     ;
      
    }
    
    this.updateSubOrganizationsList();
    this.isFormLoded = true;
    

  }
 
  newSubOrganizations(model: SubOrganization = {} as SubOrganization): import("@angular/forms").AbstractControl {
    var subOrganization = this.formBuilder.group({
      id: model.id ? model.id : 0,
      organizationId: model.organizationId ? model.organizationId : 0,
      name: model.subOrganizationName ? model.subOrganizationName : ['', [Validators.required]],
	    description: model.description ? model.description : '',     
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return subOrganization;
  }

  updateSubOrganizationsList() {
    return this.subOrganizationDataSource.next(this.getsubOrganizationList());
  }

  getsubOrganizationList() {
    return (this.form.get('subOrganizations') as FormArray).controls
  }
  onAddSubOrganization()
   {
    //const subOrganizations = this.form.get('subOrganizations') as FormArray
    
    this.subOrganizations.push(this.newSubOrganizations())
    this.updateSubOrganizationsList();   
    }
 onDeleteSubOrganization(element,index)
  {
			
	(this.form.controls['subOrganizations'] as FormArray).removeAt(index);
	this.updateSubOrganizationsList()
			
 }
	  saveChanges() 
	  {
		if (this.form.valid) {
		  this.onSaveChanges.emit(this.form.value)
		}
	   
	  }
	  
	onCancel()
	{
    this.router.navigate(['/auth/global/sub-organization-list']);
    
	}

}
