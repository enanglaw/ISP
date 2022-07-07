import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { NotesList } from '../../report.model';
import { ReportService } from '../../report.service';
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
  selector: 'app-notes-form',
  templateUrl: './notes-form.component.html',
  styleUrls: ['./notes-form.component.scss']
})
export class NotesFormComponent implements OnInit {

  sfRTEFontFamily = SFRTE.sfRteFontFamily;
  sfRTEFontSize = SFRTE.sfRteFontSize;
  sfRTETools = SFRTE.sfRteTools;
  @Input() model: NotesList;
  @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();
  allegationList: any[] = [];
  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  progress = 0;
  allegationEnquiryDataSource = new BehaviorSubject<AbstractControl[]>([]);
  allegationEnquiryDataSourceColumns: string[] = ['No', 'Title', 'Document', 'actions'];
 
  constructor(private formBuilder: FormBuilder, private reportService: ReportService,
    private dialog: MatDialog,private router: Router) 
 { }

  ngOnInit(): void
  {
    this.getAllegationList();
    
    this.inItForm();
  
  }

  get f() {
    return this.form.controls;
  }
   
  getAllegationList() {
    this.reportService.getMemorandum().subscribe(
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
        description: ['',Validators.required],
        title: [''],       
        allegationId: [0]
    });
   
    if (this.model !== undefined) {
       this.form.patchValue(this.model);
     
  }   
    
    this.isFormLoded = true;
    

  }

 
  saveChanges() {
    if (this.form.valid) {
      console.log(this.form)
      this.onSaveChanges.emit(this.form.value)
    }
   
  }

  associateAutoSearch() {

  }

  
onCancel(){
  this.router.navigate(['auth/enquiry/enquiry-list']);
}

}
