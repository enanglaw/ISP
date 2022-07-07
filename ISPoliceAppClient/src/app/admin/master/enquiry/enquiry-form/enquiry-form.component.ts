import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AllegationEnquiryDocument, EnquiryList } from '../enquiry.model';
import { EnquiryService } from '../enquiry.service';
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
  selector: 'app-enquiry-form',
  templateUrl: './enquiry-form.component.html',
  styleUrls: ['./enquiry-form.component.scss']
})
export class EnquiryFormComponent implements OnInit {
  sfRTEFontFamily = SFRTE.sfRteFontFamily;
  sfRTEFontSize = SFRTE.sfRteFontSize;
  sfRTETools = SFRTE.sfRteTools;
  @Input() model: EnquiryList;
  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();
  allegationList: any[] = [];
  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  progress = 0;
  count = 0;
  allegationEnquiryDataSource = new BehaviorSubject<AbstractControl[]>([]);
  allegationEnquiryDataSourceColumns: string[] = ['No', 'Title', 'Document', 'actions'];
 
  constructor(private formBuilder: FormBuilder, private enquiryService: EnquiryService,
    private dialog: MatDialog,private router: Router) 
 { }

  ngOnInit(): void
  {
    this.getAllegationList();
    
    this.inItForm();
  
  }
  get allegationEnquiryDocuments(): FormArray {
    return this.form.get("allegationEnquiryDocuments") as FormArray
  }
  public onDocumentFileSelected = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.enquiryService.uploadMedia(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
         const allegationEnquiryDocument = this.allegationEnquiryDocuments.at(this.count);
          allegationEnquiryDocument.patchValue
            ({
              documentUrl:event.body.dbPath
            })                    
            allegationEnquiryDocument.updateValueAndValidity();
           this.count++;

        }
      });
 }
 
 
  get f() {
    return this.form.controls;
  }
   
  getAllegationList() {
    this.enquiryService.getAllegationList().subscribe(
      (obj) => {

        this.allegationList.push(...obj);
      },
      (error) => (console.log(error))
    );
  }


  inItForm() {
    this.form = this.formBuilder.group({
        id: 0,        
        createdDate: [new Date()],
        outCome: [''],
        mom: [''],
        participant: [''],
        title: ['', Validators.required],       
        allegationId: [0],      
        allegationEnquiryDocuments: new FormArray([
      ])
    });
   
    if (this.model !== undefined) {
       this.form.patchValue(this.model);
      let allegationEnquiryDocumentForm = (this.form.get('allegationEnquiryDocuments') as FormArray);
      allegationEnquiryDocumentForm.clear();
      let enquiryDocumentForm = this.model.allegationEnquiryDocuments;
      enquiryDocumentForm.forEach(element => {
        allegationEnquiryDocumentForm.push(this.newAllegationEnquiryDocumentForm(element))
      });
  }   
    
    this.updateAllegationEnquiryDocumentList();
    
    this.isFormLoded = true;
    

  }
 
  newAllegationEnquiryDocumentForm(model: AllegationEnquiryDocument = {} as AllegationEnquiryDocument): import("@angular/forms").AbstractControl {
    var allegationEnquiry = this.formBuilder.group({
      id: model.id ? model.id : 0,
      allegationEnquiryId: model.allegationEnquiryId ? model.allegationEnquiryId : 0,
      createdDate: model.createdDate ? model.createdDate : [new Date()],
      title: model.title ? model.title:['', Validators.required],
      documentUrl: model.documentUrl ? model.documentUrl : [''],
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })
       
    return allegationEnquiry;
  }
  updateAllegationEnquiryDocumentList() {
    this.allegationEnquiryDataSource.next(this.getAllegationEnquiryDocumentList());
  }
  getAllegationEnquiryDocumentList() {
    return (this.form.get('allegationEnquiryDocuments') as FormArray).controls
  }
  onAllegationEnquiryDocumentFormDelete(element,index) {
    
    (this.form.controls['allegationEnquiryDocuments'] as FormArray).removeAt(index);
    this.updateAllegationEnquiryDocumentList()
  }

  onAddAllegationEnquiryDocumentForm() {
    const allegationEnquiryDocument = this.form.get('allegationEnquiryDocuments') as FormArray
    allegationEnquiryDocument.push(this.newAllegationEnquiryDocumentForm())
    this.updateAllegationEnquiryDocumentList();
  }

 
  saveChanges() {
    if (this.form.valid) {
      this.onSaveChanges.emit(this.form.value)
    }
   
  }

  associateAutoSearch() {

  }

  
 
 
  
  comparePSObjects(object1: any, object2: any) {
    return object1 && object2 && object1.stationId == object2.stationId;
}
onCancel(){
  this.router.navigate(['auth/enquiry/enquiry-list']);
}

}
