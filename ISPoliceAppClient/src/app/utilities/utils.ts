import {
  AbstractControl,
  FormArray,
  FormControl,
  FormGroup,
  ValidationErrors,
} from '@angular/forms';

export function toBase64(file: File) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });
}

export function parseWebAPIErrors(response: any): string[] {
  const result: string[] = [];

  if (response.error) {
    if (typeof response.error === 'string') {
      result.push(response.error);
    } else if (Array.isArray(response.error)) {
      response.error.forEach((value) => result.push(value.description));
    } else {
      const mapErrors = response.error.errors;
      const entries = Object.entries(mapErrors);
      entries.forEach((arr: any[]) => {
        const field = arr[0];
        arr[1].forEach((errorMessage) => {
          result.push(`${field}: ${errorMessage}`);
        });
      });
    }
  }

  return result;
}

export function formatDateFormData(date: Date) {
  date = new Date(date);
  const format = new Intl.DateTimeFormat('en', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
  });

  const [{ value: month }, , { value: day }, , { value: year }] =
    format.formatToParts(date);

  // yyyy-MM-dd
  return `${year}-${month}-${day}`;
}

export interface FieldError {
  formGroupName: string;
  fieldName: string;
  errorCode: string;
}

export function getFormErrors(
  control: AbstractControl,
  formGroupName: string,
  fieldName: string,
  errors: FieldError[]
) {
  if (control instanceof FormGroup) {
    Object.keys(control.controls).forEach((controlName) => {
      let formControl = control.get(controlName);
      if (formControl) {
        let fGroupName = formGroupName + '-' + controlName;
        getFormErrors(formControl, fGroupName, controlName, errors);
      }
    });
  }

  if (control instanceof FormArray) {
    control.controls.forEach((fControl: AbstractControl, index) => {
      let fGroupName = formGroupName + '-' + index;
      getFormErrors(fControl, fGroupName, 'Array', errors);
    });
  }

  if (control instanceof FormControl) {
    const controlErrors: ValidationErrors | null = control.errors;
    if (controlErrors) {
      Object.keys(controlErrors).forEach((errorCode) => {
        errors.push({
          formGroupName: formGroupName,
          fieldName: fieldName,
          errorCode: errorCode,
        });
      });
    }
  }
}

export function compareSubCategoryToSort(ob1, ob2) {
  if (ob1.subCategoryName < ob2.subCategoryName) return -1;
  if (ob1.subCategoryName > ob2.subCategoryName) return 1;
  return 0;
}

export const toolBar = {
  toolbar: [
    'heading',
    '|',
    'fontfamily',
    'fontsize',
    'alignment',
    'fontColor',
    'fontBackgroundColor',
    '|',
    'bold',
    'italic',
    'strikethrough',
    'underline',
    'subscript',
    'superscript',
    '|',
    'link',
    '|',
    'outdent',
    'indent',
    '|',
    'bulletedList',
    '-',
    'numberedList',
    'todoList',
    '|',
    'code',
    'codeBlock',
    '|',
    'insertTable',
    '|',
    'imageUpload',
    'blockQuote',
    '|',
    'todoList',
    'undo',
    'redo',
  ],
};

export const ckEditorToolbarConfig = {
  // additionalPlugins: [Alignment],
  toolbar: {
    items: [
      'heading',
      '|',
      'fontfamily',
      'fontsize',
      'alignment',
      'fontColor',
      'fontBackgroundColor',
      '|',
      'bold',
      'italic',
      'strikethrough',
      'underline',
      'subscript',
      'superscript',
      '|',
      'link',
      '|',
      'outdent',
      'indent',
      '|',
      'bulletedList',
      '-',
      'numberedList',
      'todoList',
      '|',
      'code',
      'codeBlock',
      '|',
      'insertTable',
      '|',
      'imageUpload',
      'blockQuote',
      '|',
      'todoList',
      'undo',
      'redo',
    ],
    // plugins: [],
    shouldNotGroupWhenFull: true,
  },
  image: {
    // Configure the available styles.
    styles: ['alignLeft', 'alignCenter', 'alignRight'],

    // Configure the available image resize options.
    resizeOptions: [
      {
        name: 'resizeImage:original',
        label: 'Original',
        value: null,
      },
      {
        name: 'resizeImage:50',
        label: '25%',
        value: '25',
      },
      {
        name: 'resizeImage:50',
        label: '50%',
        value: '50',
      },
      {
        name: 'resizeImage:75',
        label: '75%',
        value: '75',
      },
    ],

    // You need to configure the image toolbar, too, so it shows the new style
    // buttons as well as the resize buttons.
    toolbar: [
      'imageStyle:alignLeft',
      'imageStyle:alignCenter',
      'imageStyle:alignRight',
      '|',
      'ImageResize',
      '|',
      'imageTextAlternative',
    ],
  },
  language: 'en',
};

// region syncfusion RichTextEditor attribute values
export const sfRteTools: object = {
  type: 'MultiRow',
  items: [
    'Undo',
    'Redo',
    '|',
    'Bold',
    'Italic',
    'Underline',
    'StrikeThrough',
    '|',
    'FontName',
    'FontSize',
    'FontColor',
    'BackgroundColor',
    '|',
    'SubScript',
    'SuperScript',
    '|',
    'LowerCase',
    'UpperCase',
    '|',
    'Formats',
    'Alignments',
    '|',
    /* 'OrderedList',
    'UnorderedList', */
    'NumberFormatList',
    'BulletFormatList',
    '|',
    'Indent',
    'Outdent',
    '|',
    'CreateLink',
    'Image',
    'CreateTable',
    '|',
    'ClearFormat',
    'Print',
    'SourceCode',
    '|',
    'FullScreen',
  ],
};
  
export const sfRteFontSize = {
  width: '40px',
  default: '13pt',
  items: [
    { text: '8 pt', value: '8pt' },
    { text: '10 pt', value: '10pt' },
    { text: '12 pt', value: '12pt' },
    { text: '13 pt', value: '13pt' },
    { text: '14 pt', value: '14pt' },
    { text: '18 pt', value: '18pt' },
    { text: '24 pt', value: '24pt' },
    { text: '36 pt', value: '36pt' },
  ],
};
 
export const sfRteFontFamily = {
  width: '60px',
  default: 'Arial',
  items: [
    { text: 'Impact', value: 'Impact,Charcoal,sans-serif' },
    { text: 'Arial', value: 'Arial,Helvetica,sans-serif' },
    { text: 'Segoe UI', value: 'Segoe UI' },
    { text: 'Courier New', value: 'Courier New,Courier,monospace' },
    { text: 'Georgia', value: 'Georgia,serif' },
    { text: 'Calibri Light', value: 'CalibriLight' },
    { text: 'Tahoma', value: 'Tahoma,Geneva,sans-serif' }
  ],
};
// endregion