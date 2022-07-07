import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AllegationList } from '../allegation.model';
import { AllegationService } from '../allegation.service';
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
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-allegation-form',
  templateUrl: './allegation-form.component.html',
  styleUrls: ['./allegation-form.component.scss']
})
export class AllegationFormComponent implements OnInit {

 
  sfRTEFontFamily = SFRTE.sfRteFontFamily;
  sfRTEFontSize = SFRTE.sfRteFontSize;
  sfRTETools = SFRTE.sfRteTools; 
  @Input() model: AllegationList;
  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();
  
  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  count: number=0;
  progress = 0;
  constructor(private formBuilder: FormBuilder,
    private allegationService: AllegationService,
    private dialog: MatDialog, private router: Router,) { }

  ngOnInit(): void {
    this.inItForm();
  }
  public onDocumentFileSelected = (files) => {

    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.allegationService.uploadMedia(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.form.patchValue({
            attachmentUrl: event.body.dbPath
          });
        }
      });
  }
  
  saveChanges() {
    if (this.form.valid) {      
     
      this.onSaveChanges.emit(this.form.value)
    }
   
  }
  onCancel() {
    this.router.navigate(['/auth/allegation/allegation-list'])
  }
  inItForm() {
    this.form = this.formBuilder.group({
   
      id: [0],  
      title:['',{validators:[Validators.required]}],
       complainant:['', { validators: [Validators.required] }],
       personalProfileId:[0],
       dateOfComplaint:[new Date()],       
       accusedName:['', { validators: [Validators.required] }],       
       accusedPosting:['', { validators: [Validators.required] }],      
       accusedRank:[''],       
       complaintDetails:[''],
       attachmentUrl:[''],
    });
    
    if (this.model !== undefined) {
      
      this.form.patchValue(this.model)      

      
    }
    this.isFormLoded = true;
    

  }
}
