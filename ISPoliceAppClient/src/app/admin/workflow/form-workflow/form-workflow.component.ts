import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { workflowCreationDTO } from '../workflow.models';

@Component({
  selector: 'app-form-workflow',
  templateUrl: './form-workflow.component.html',
  styleUrls: ['./form-workflow.component.scss'],
})
export class FormWorkflowComponent implements OnInit {
  constructor(private formBuilder: FormBuilder) {}

  @Input()
  model: workflowCreationDTO;

  form: FormGroup;

  @Output()
  onSaveChanges: EventEmitter<workflowCreationDTO> = new EventEmitter<
    workflowCreationDTO
  >();

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      workflowName: [
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
    const field = this.form.get('workflowName');

    if (field!.hasError('required')) {
      return 'The name field is required';
    }

    if (field!.hasError('minlength')) {
      return 'The minimum length is 3';
    }

    return '';
  }
}
