import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { OrganizationEventDTO, OrganizationModelList, SubOrganization, SubOrganizationModelList } from '../organization.model';
import { OrganizationService } from '../organization.service';
import {
  QuickToolbarService,
  ToolbarService,
  LinkService,
  ImageService,
  HtmlEditorService,
  TableService,
  RichTextEditorComponent,
} from '@syncfusion/ej2-angular-richtexteditor';
import * as SFRTE from 'src/app/utilities/utils';
@Component({
  selector: 'app-events-form',
  templateUrl: './events-form.component.html',
  styleUrls: ['./events-form.component.scss']
})
export class EventsFormComponent implements OnInit {

  sfRTEFontFamily = SFRTE.sfRteFontFamily;
  sfRTEFontSize = SFRTE.sfRteFontSize;
  sfRTETools = SFRTE.sfRteTools;
  @Input() model: OrganizationEventDTO;
  @Input() organizationListmodel: OrganizationModelList[]=[];
  @Input() subOrganizationListmodel: SubOrganizationModelList[]=[];
  
 @Input() heading: any;
 @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();

 isFormLoded: boolean = false;
 showLoading: boolean = false;
 form: FormGroup;
 progress = 0;
 

  constructor(private formBuilder: FormBuilder,
    private profileService: OrganizationService,
    private dialog: MatDialog, private router:
      Router, private ref: ChangeDetectorRef) {

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
  this.profileService.getOrganizationById(Id).subscribe(    
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
  this.profileService.getOrganizationDropdown().subscribe(
  
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
     id:[0], 
     title: ['', { validators: [Validators.required] }],
     description: ['', { validators: [Validators.required] }],
     eventDate: [new Date()],     
     organizationId: [0],
     organizationName:[''],
     subOrganizationId: [0],
     subOrganizationName:['']

    
   });
   
   if (this.model !== undefined)
   {
     this.form.patchValue(this.model);
   }
   
   this.isFormLoded = true;
   

 }

 
   saveChanges() 
   {
  
     if (this.form.valid)
     {
      this.onSaveChanges.emit(this.form.value)
     }
    
   }
   
 onCancel()
 {
   this.router.navigate(['/auth/organization/events']);
 }

}
