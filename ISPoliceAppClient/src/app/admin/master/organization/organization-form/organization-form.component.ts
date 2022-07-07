import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router'
import { OrganizationService } from '../organization.service';
import { OrganizationEvent, OrganizationList, OrganizationMedia, SubOrganization } from '../organization.model';
import { HttpEventType } from '@angular/common/http';


@Component({
  selector: 'app-organization-form',
  templateUrl: './organization-form.component.html',
  styleUrls: ['./organization-form.component.scss']
})
export class OrganizationFormComponent implements OnInit
{
  

  @Input() model: OrganizationList;
  @Input() organizationListmodel: OrganizationList[]=[];
  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();

  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  count: number=0;
  progress = 0;
  subOrganizationDataSource = new BehaviorSubject<AbstractControl[]>([]);
  subOrganizationDataSourceColumns: string[] = ['No', 'Name', 'Description', 'actions'];
  /*organizationMediaDataSource = new BehaviorSubject<AbstractControl[]>([]);
  organizationMediaDataSourceColumns: string[] = ['No', 'Title','Document', 'actions'];*/
  organizationEventDataSource = new BehaviorSubject<AbstractControl[]>([]);
  organizationEventDataSourceColumns: string[] = ['No', 'Title', 'Description','Event Date', 'actions'];
  
  constructor(private formBuilder: FormBuilder,
    private organizationService: OrganizationService,
    private dialog: MatDialog, private router: Router,
    private ref: ChangeDetectorRef)
  {

  }
 
  ngOnInit(): void {
   
    this.inItForm();
 
  }
  get organizationMedia(): FormArray {
    return this.form.get("organizationMedia") as FormArray
  }
  public onMediaFileSelected = (files) => {
  
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.organizationService.uploadMedia(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
         
         const media = this.organizationMedia.at(this.count);
          media.patchValue
            ({
              mediaUrl:event.body.dbPath
            })
            media.updateValueAndValidity();
            this.count++;
        }
      });
  }
  public onSymbolFileSelected = (files) => {
  
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.organizationService.uploadSymbol(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
         
          this.form.patchValue({
            symbolUrl: event.body.dbPath
          });
        }
      });
  }


  public onFlagFileSelected = (files) => {

    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.organizationService.uploadFlag(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.form.patchValue({
            flagUrl: event.body.dbPath
          });
        }
      });
  }

  
  inItForm() {
    this.form = this.formBuilder.group({
      organizationId: 0,
      shortName: [''],
      fullName: ['', { validators: [Validators.required] }],
      ideology: ['', { validators: [Validators.required] }],
      flagUrl: [''],
      symbolUrl: [''],
      isActive: [true],      
      subOrganizationCategory: new FormArray([
       
      ]),
      organizationEvent: new FormArray([
       
      ]),
    });
    
    if (this.model !== undefined) {
  
      let subOrg = this.model.subOrganizationCategory;
     
      this.form.patchValue(this.model)
        
      let subOrganization = (this.form.get('subOrganizationCategory') as FormArray);
      subOrganization.clear();  
      subOrg.forEach(element => {
        subOrganization.push(this.newSubOrganization(element))
      });
      

      /*let organizationMedia = (this.form.get('organizationMedia') as FormArray);
      organizationMedia.clear();
      this.model.organizationMedia.filter(a=>a.isActive).forEach(element => {
        organizationMedia.push(this.newOrganizationMedia(element))
      });*/

      let organizationEvents = (this.form.get('organizationEvent') as FormArray);
      let event=this.model.organizationEvent;
      organizationEvents.clear();
      event.forEach(element => {
        organizationEvents.push(this.newOrganizationEvents(element))
      })
    }
   
    this.UpdateOrganizationEvent();
    this.updateSubOrganizationList();
    /*this.updateOrganizationMediaList()*/
    this.isFormLoded = true;
    

  }
 
  newSubOrganization(model: SubOrganization = {} as SubOrganization): import("@angular/forms").AbstractControl {
    var subOrganization = this.formBuilder.group({
      id: model.id ? model.id : 0,
      organizationId: model.organizationId ? model.organizationId : 0,
      name: model.name ? model.name : ['', Validators.required],
      description: model.description ? model.description :'',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })
    return subOrganization;
  }

  updateSubOrganizationList() {
    return this.subOrganizationDataSource.next(this.getSubOrganizationList());
  }

  getSubOrganizationList() {
    return (this.form.get('subOrganizationCategory') as FormArray).controls
  }

  onSubOrganizationDelete(element,index) {    
    (this.form.controls['subOrganizationCategory'] as FormArray).removeAt(index);
    this.updateSubOrganizationList()
  }

  onAddSubOrganizations() {
    const subOrganization = this.form.get('subOrganizationCategory') as FormArray
    subOrganization.push(this.newSubOrganization())
    this.updateSubOrganizationList();
  }
/*
  newOrganizationMedia(model: OrganizationMedia = {} as OrganizationMedia): import("@angular/forms").AbstractControl {
    var organizationMedia = this.formBuilder.group({
      id: model.id ? model.id : 0,
      organizationId: model.organizationId ? model.organizationId : 0,
      title: model.title ? model.title : ['', Validators.required],
      mediaUrl:model.mediaUrl? model.mediaUrl:['', Validators.required],
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return organizationMedia;
  }

  updateOrganizationMediaList() {
    this.organizationMediaDataSource.next(this.getOrganizationMediaList());
  }

  getOrganizationMediaList() {
    return (this.form.get('organizationMedia') as FormArray).controls
  }

  onOrganizationMediaDelete(element,index) {
    
    (this.form.controls['organizationMedia'] as FormArray).removeAt(index);
    this.updateOrganizationMediaList()
  }

  onAddOrganizationMedia() {
    const organizationMedia = this.form.get('organizationMedia') as FormArray
    organizationMedia.push(this.newOrganizationMedia())
    this.updateOrganizationMediaList();
  }
*/
  newOrganizationEvents(model: OrganizationEvent = {} as OrganizationEvent): import("@angular/forms").AbstractControl {
    var organizationEvent = this.formBuilder.group({
      id: model.id ? model.id : 0,
      organizationId: model.organizationId ? model.organizationId : 0,
      description: model.description ? model.description : '',
      eventDate: model.eventDate ? model.eventDate : '',
      title: model.title ? model.title : ['', Validators.required],

      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return organizationEvent;
  }


  UpdateOrganizationEvent() {
    this.organizationEventDataSource.next(this.getOrganizationEventsList());
  }

  getOrganizationEventsList() {
    return (this.form.get('organizationEvent') as FormArray).controls
  }

  onOrganizationEventDelete(element,index) {
    (this.form.controls['organizationEvent'] as FormArray).removeAt(index);
    this.UpdateOrganizationEvent();
  }

  onAddOrganizationEvent() {
    const organizationEvent = this.form.get('organizationEvent') as FormArray
    organizationEvent.push(this.newOrganizationEvents())
    this.UpdateOrganizationEvent();
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
