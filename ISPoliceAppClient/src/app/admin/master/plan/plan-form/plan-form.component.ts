import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router'
import { HttpEventType } from '@angular/common/http';
import { PlanService } from '../plan.service';
import { PlanList } from '../plan.model';


@Component({
  selector: 'app-plan-form',
  templateUrl: './plan-form.component.html',
  styleUrls: ['./plan-form.component.scss']
})
export class PlanFormComponent implements OnInit {

  @Input() model: PlanList;
  @Input() organizationListmodel: PlanList[]=[];
  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();

  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  progress = 0;
  planDataSource = new BehaviorSubject<AbstractControl[]>([]);
  planDataSourceColumns: string[] = ['No', 'Title','Document', 'actions'];
  imageUrl = "assets/images/avatars/noavatar.png";
  constructor(private formBuilder: FormBuilder,
    private planService: PlanService,
    private dialog: MatDialog, private router: Router,
    private ref: ChangeDetectorRef)
  {

  }
 
  ngOnInit(): void {
   
    this.inItForm();
 
  }
  
  get plans(): FormArray {
    return this.form.get("plans") as FormArray
  }
  
  public uploadFile = (files) => {
    console.log(files);
   
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.planService.uploadUrl(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
         
          const plan = this.plans.at(0);
          plan.patchValue
            ({
              documentUrl:event.body.dbPath
            })
            plan.updateValueAndValidity();
        }
      });
  }

  inItForm() {
    this.form = this.formBuilder.group({       
          plans: new FormArray([]),
    });
    
    if (this.model !== undefined) {     
      let organizationMedia = (this.form.get('plans') as FormArray);
      organizationMedia.clear();     
      organizationMedia.push(this.newPlan(this.model))   
         
    }
   
    this.updatePlanList()
    this.isFormLoded = true;
    

  }
 
  newPlan(model: PlanList = {} as PlanList): import("@angular/forms").AbstractControl {
    var plan = this.formBuilder.group({
      id: model.id ? model.id : 0,
      title: model.title ? model.title : ['', Validators.required],
      documentUrl:model.documentUrl? model.documentUrl:['',Validators.required],
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return plan;
  }

  updatePlanList() {
    this.planDataSource.next(this.getPlanList());
  }

  getPlanList() {
    return (this.form.get('plans') as FormArray).controls
  }

  onPlanDelete(element,index) {
    
    (this.form.controls['plans'] as FormArray).removeAt(index);
    this.updatePlanList()
  }

  onAddPlan() {
    const plan = this.form.get('plans') as FormArray
    plan.push(this.newPlan())
    this.updatePlanList();
  }


  saveChanges() {
    if (this.form.valid) {
      this.onSaveChanges.emit(this.form.value)
    }
   
  }

onCancel(){
  this.router.navigate(['/auth/organization/org-list']);
}

}
