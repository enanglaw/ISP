import { HttpEventType } from '@angular/common/http';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { AbstractCreationDTO,AbstractUpdateDTO } from '../../global.model';
import { GlobalService } from '../../global.service'; 
import { startWith, debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router'

@Component({
  selector: 'app-gender-form',
  templateUrl: './gender-form.component.html',
  styleUrls: ['./gender-form.component.scss']
})
export class GenderFormComponent implements OnInit {

  @Input() model: AbstractCreationDTO;

   @Input() heading: any;
  @Output() onSaveChanges: EventEmitter<any> = new EventEmitter<any>();

  isFormLoded: boolean = false;
  showLoading: boolean = false;
  form: FormGroup;
  progress = 0;


  constructor(private formBuilder: FormBuilder, private profileService: GlobalService,
    private dialog: MatDialog,private router: Router,private ref: ChangeDetectorRef) {
   

  }

  ngOnInit(): void {
    this.inItForm();
  }
  inItForm() {

    this.form = this.formBuilder.group({
      id: 0,
      name: ['', { validators: [Validators.required] }]
    })
    this.isFormLoded = true;
  }
  get f() {
    return this.form.controls;
  }
  saveChanges() {
    if (this.form.valid) {
      this.onSaveChanges.emit(this.form.value)
    }
   
  }
  onCancel(){
    this.router.navigate(['auth/global/gender-list']);
  }
}
