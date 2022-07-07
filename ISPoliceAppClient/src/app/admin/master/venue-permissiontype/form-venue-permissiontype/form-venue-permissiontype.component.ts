import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  VenueDropdownDTO,
  VenuePermissionTypeCreationDTO,
} from 'src/app/admin/master/venue.modal';

@Component({
  selector: 'app-form-venue-permissiontype',
  templateUrl: './form-venue-permissiontype.component.html',
  styleUrls: ['./form-venue-permissiontype.component.scss'],
})
export class FormVenuePermissiontypeComponent implements OnInit {
  @Input()
  model: VenuePermissionTypeCreationDTO;
  @Input()
  venueList: VenueDropdownDTO[] = [];
  @Output()
  onSaveChanges: EventEmitter<VenuePermissionTypeCreationDTO> = new EventEmitter<VenuePermissionTypeCreationDTO>();

  form: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      venuePermissionTypeName: ['', { validators: [Validators.required] }],
      venueId: ['', { validators: [Validators.required] }],
      isActive: ['true', { validators: [] }],
    });

    if (this.model !== undefined) {
      this.form.patchValue(this.model);
    }
  }

  saveChanges() {
    this.onSaveChanges.emit(this.form.value);
  }

  get f() {
    return this.form.controls;
  }
}
