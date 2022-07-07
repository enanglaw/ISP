import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BehaviorSubject } from 'rxjs';
import { ProfileAlias } from '../../profile.model';
import { ProfileService } from '../../profile.service';

@Component({
  selector: 'app-new-associate',
  templateUrl: './new-associate.component.html',
  styleUrls: ['./new-associate.component.scss']
})
export class NewAssociateComponent implements OnInit {
  associateForm: any;
  initFormComplete: boolean = false;
  showLoading:boolean=false;
  newProfileAssociateAliasDataSource = new BehaviorSubject<AbstractControl[]>([]);
  newProfileAssociateAliasDataSourceColumns: string[] = ['No', 'Name', 'actions'];

  constructor(private formBuilder: FormBuilder, private profileService: ProfileService,
    private dialogRef: MatDialogRef<NewAssociateComponent>,
    @Inject(MAT_DIALOG_DATA) data) { }

  ngOnInit(): void {
    this.inItAssociateForm();
  }
  inItAssociateForm() {
    this.associateForm = this.formBuilder.group({
      Id: 0,
      name: ['', { validators: [Validators.required] }],
      image: [''],
      hs: [''],
      entryDate: [new Date()],
      isActive: [true],
      profileAlias: new FormArray([
        //this.newProfileAlias()
      ]),

    });
    this.updateNewProfileAssociateAliasList();
    this.initFormComplete = true;
  }
  get newAssiciate() {
    return this.associateForm.controls;
  }

  newProfileAlias(model: ProfileAlias = {} as ProfileAlias): import("@angular/forms").AbstractControl {
    var profileAlias = this.formBuilder.group({
      id: model.id ? model.id : 0,
      profileId: model.profileId ? model.profileId : 0,
      name: model.name ? model.name : '',
      isActive: model.hasOwnProperty('isActive') ? model.isActive ? true : false : true,
    })

    return profileAlias;
  }
  save() {
    this.dialogRef.close(this.associateForm.value);
  }

  close() {
    this.dialogRef.close();
  }
  updateNewProfileAssociateAliasList() {
    this.newProfileAssociateAliasDataSource.next(this.getNewProfileAssociateAliasList());
  }
  getNewProfileAssociateAliasList() {
    return (this.associateForm.get('profileAlias') as FormArray).controls
  }
  onNewProfileAssociateAliasDelete(element) {
    element.patchValue({
      isActive: false
    });
    // this.accusedList.removeAt(id);
    this.newProfileAssociateAliasDataSource.next(this.getProfileAliasList());
  }
  onAddNewProfileAssociateAlias() {
    const profileAlias = this.associateForm.get('profileAlias') as FormArray
    profileAlias.push(this.newProfileAlias())
    this.updateNewProfileAssociateAliasList();
  }
  getProfileAliasList() {
    return (this.associateForm.get('profileAlias') as FormArray).controls
  }
  addNewAssociate() {
    this.showLoading=true;
    if (this.associateForm.valid) {
      this.profileService.addQuickProfile(this.associateForm.value)
        .subscribe(resul => {
          this.showLoading=false;
          this.dialogRef.close(resul);
        })
    }
  }
}
