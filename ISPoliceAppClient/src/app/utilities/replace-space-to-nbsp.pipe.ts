import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'replaceSpaceToNbsp'})
export class ReplaceSpaceToNbsp implements PipeTransform {
  transform(value: string | null): string {
    if (value === null)
      return '';
    return value.replace(/\s/g, "&nbsp;");
  }
}