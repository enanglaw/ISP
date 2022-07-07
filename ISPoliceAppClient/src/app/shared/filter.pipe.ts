import { Pipe, PipeTransform } from '@angular/core';
// import { Member } from '../pages/gang/gang.model';

@Pipe({
  name: 'filter',
})
export class FilterPipe implements PipeTransform {
  transform(items: Member[], field: string, value: boolean): any[] {
    
    if (!items) return [];
    // if (!value || value.length == 0) return items;
    console.log(items.filter((it) => it[field] == value));
    return items.filter((it) => it[field] == value);
  }
}

export interface Member {
  id: number;
  memberName: string;
}
