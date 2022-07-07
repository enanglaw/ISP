import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FieldError, getFormErrors } from 'src/app/utilities/utils';
import Swal from 'sweetalert2';
import { GangDropdownDTO } from '../../gang/gang.model';
import {
  CaseDTO,
  CaseStatusDTO,
  PersonStatusDTO,
  PersonTypeDTO,
} from '../../input-entry/input-entry.model';
import { PersonCreationDTO } from '../person.model';
import { PersonService } from '../person.service';
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
  selector: 'app-create-person',
  templateUrl: './create-person.component.html',
  styleUrls: ['./create-person.component.scss'],
  providers: [
    QuickToolbarService,
    ToolbarService,
    LinkService,
    ImageService,
    HtmlEditorService,
    TableService,
  ],
})
export class CreatePersonComponent implements OnInit {
  sfRTEFontFamily = SFRTE.sfRteFontFamily;
  sfRTEFontSize = SFRTE.sfRteFontSize;
  sfRTETools = SFRTE.sfRteTools;
  errors: string[] = [];
  requestApplicationDocument: File | null;
  model: PersonCreationDTO;
  casesList: CaseDTO[] = [];
  caseStatusList: CaseStatusDTO[] = [];
  personPersonTypeList: PersonTypeDTO[] = [];
  personStatusList: PersonStatusDTO[] = [];
  gangList: GangDropdownDTO[] = [];
  rivalGangList: GangDropdownDTO[] = [];
  showRemoveButton = false;
  inputTypeList: any[] = [];
  showEngDocRemoveButton = false;
  showTamilDocRemoveButton = false;
  selectedFiles: string[] = [];
  inputEntryId: number;
  formErrors: FieldError[] = [];
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private personService: PersonService,
    private router: Router
  ) {}

  ngOnInit(): void {
    
    getFormErrors(this.form, 'form', '', this.formErrors);

    this.personService.getCases().subscribe((obj) => {
      console.log('Cases', obj);
      this.casesList.push(...obj);
    });

    this.personService.getCaseStatuses().subscribe((obj) => {
      console.log('CaseStatuses', obj);
      this.caseStatusList.push(...obj);
    });

    this.personService.getPersonType().subscribe((obj) => {
      console.log('Person Type', obj);
      this.personPersonTypeList.push(...obj);
    });

    this.personService.getPersonStatus().subscribe((obj) => {
      console.log('Person Status', obj);
      this.personStatusList.push(...obj);
    });

    this.personService.getGangDropdownData().subscribe((obj) => {
      console.log('Gangs', obj);
      this.gangList.push(...obj);
    });

    this.form = this.formBuilder.group({
      personName: ['', { validators: [Validators.required] }],
      parentName: [''],
      personAliasName: this.formBuilder.array([]),
      primaryAddress: [''],
      personAddress: this.formBuilder.array([
        /* this.createAddressGroup() */
      ]),
      personCaseHistory: this.formBuilder.array([
        /* this.createCaseHistory() */
      ]),
      personPersonType: [''],
      historySheetNo: [''],
      currentActivity: [''],
      statusId: [''],
      modusOperandi: [''],
      gangId: [''],
      gangMemberType: [''],
      personRivalGang: this.formBuilder.array([
        /* this.createRivalGang() */
      ]),
      photoDocument: [null],
      personMedia: this.formBuilder.array([]),
    });

    if (this.model !== undefined) {
      //this.form.patchValue(this.model);
      Object.keys(this.model).forEach((name) => {
        console.log(name);
        if (this.form.controls[name]) {
          console.log(name);
          this.form.controls[name].patchValue(this.model[name], {
            onlySelf: true,
          });
        }
      });
    }
  }

  saveChanges() {
    //console.log(this.form.value);
    
    var formData = Utility.convertModelToFormData(this.form.value);
    console.log(formData);
    this.personService.create(this.form.value).subscribe(
      (obj) => {
        console.log(obj);
        Swal.fire(
          'Success',
          'Person Profile saved successfully',
          'success'
        ).then((result) => {
          this.router.navigate(['/auth/person']);
        });
      },
      (err) => (this.errors = err)
    );
  }

  addAliasName() {
    this.alias.push(this.formBuilder.control(null, [Validators.required]));
  }

  removeAliasName(index) {
    this.alias.removeAt(index);
  }

  addAddress() {
    this.addresses.push(this.createAddressGroup());
  }

  removeAddress(index) {
    this.addresses.removeAt(index);
  }

  addCaseHistory() {
    this.caseHistory.push(this.createCaseHistory());
  }

  removeCaseHistory(index) {
    this.caseHistory.removeAt(index);
  }

  addRivalGang() {
    this.rivalGangs.push(this.createRivalGang());
  }

  removeRivalGang(index) {
    this.rivalGangs.removeAt(index);
  }

  addInputMedia() {
    this.inputMedias.push(this.createInputMedia());
  }

  removeInputMedia(index) {
    this.inputMedias.removeAt(index);
    this.selectedFiles.splice(index, 1);
  }

  createAddressGroup(): FormGroup {
    return this.formBuilder.group({
      addressLabel: [null, [Validators.required]],
      addressText: [null, [Validators.required]],
    });
  }

  createCaseHistory() {
    return this.formBuilder.group({
      caseId: [null, [Validators.required]],
      caseStatusId: [null, [Validators.required]],
    });
  }

  createRivalGang() {
    return this.formBuilder.group({
      rivalGangId: [null, [Validators.required]],
    });
  }

  createInputMedia() {
    this.selectedFiles.push('Click here to upload the Input Media');
    console.log('New Media control added', this.selectedFiles);
    return this.formBuilder.group({
      mediaLabel: [null, [Validators.required]],
      media: [null, [Validators.required]],
    });
  }

  get myForm() {
    return this.formBuilder;
  }

  get f() {
    return this.form.controls;
  }

  /* updateFileLabel(event) {
      if (event.target.files.length > 0) {
        const file: File = event.target.files[0];
        this.requestApplicationDocument = file;
        this.form.controls['inputDocument'].setValue(file);
        this.selectedFile = file.name;
        this.showRemoveButton = true;
      }
    } */

  updateMediaFileLabel(index, event) {
    if (event.target.files.length > 0) {
      const file: File = event.target.files[0];
      this.inputMedias.controls[index]?.get('media')?.setValue(file);
      this.selectedFiles[index] = file.name;
      console.log(this.selectedFiles);
      this.showRemoveButton = true;
    }
  }

  get alias() {
    return this.form.get('personAliasName') as FormArray;
  }

  get addresses() {
    return this.form.get('personAddress') as FormArray;
  }

  get caseHistory() {
    return this.form.get('personCaseHistory') as FormArray;
  }

  get rivalGangs() {
    return this.form.get('personRivalGang') as FormArray;
  }

  get inputMedias() {
    return this.form.get('personMedia') as FormArray;
  }

  myFormErrors() {
    this.formErrors = [];
    getFormErrors(this.form, 'form', '', this.formErrors);
  }

  onImageSelected(file: File) {
    console.log(file.name);
    this.form.controls.photoDocument.setValue(file);
    this.form.controls.photoDocument.updateValueAndValidity();
  }
}

export const Utility = {
  convertModelToFormData(val, formData = new FormData(), namespace = '') {
    if (typeof val !== 'undefined' && val !== null) {
      if (val instanceof Date) {
        formData.append(namespace, val.toISOString());
      } else if (val instanceof Array) {
        for (let i = 0; i < val.length; i++) {
          this.convertModelToFormData(
            val[i],
            formData,
            namespace + '[' + i + ']'
          );
        }
      } else if (typeof val === 'object' && !(val instanceof File)) {
        for (let propertyName in val) {
          if (val.hasOwnProperty(propertyName)) {
            this.convertModelToFormData(
              val[propertyName],
              formData,
              namespace ? `${namespace}[${propertyName}]` : propertyName
            );
          }
        }
      } else if (val instanceof File) {
        formData.append(namespace, val);
      } else {
        formData.append(namespace, val.toString());
      }
    }
    return formData;
  },
};
