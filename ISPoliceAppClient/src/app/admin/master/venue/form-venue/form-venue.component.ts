import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VenueCreationDTO } from 'src/app/admin/master/venue.modal';

@Component({
  selector: 'app-form-venue',
  templateUrl: './form-venue.component.html',
  styleUrls: ['./form-venue.component.scss'],
})
export class FormVenueComponent implements OnInit {
  @Input()
  model: VenueCreationDTO;

  @Output()
  onSaveChanges: EventEmitter<VenueCreationDTO> = new EventEmitter<VenueCreationDTO>();

  form: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      venueName: [
        '',
        {
          validators: [Validators.required],
        },
      ],
      isActive: [
        'true',
        {
          validators: [],
        },
      ],
    });

    if (this.model !== undefined) {
      this.form.patchValue(this.model);
    }
  }

  saveChanges() {
    this.onSaveChanges.emit(this.form.value);
  }

  getErrorMessageFieldName() {
    const field = this.form.get('venueName');

    if (field!.hasError('required')) {
      return 'The venue name is required';
    }

    if (field!.hasError('minlength')) {
      return 'The minimum length is 3';
    }

    return '';
  }
}
