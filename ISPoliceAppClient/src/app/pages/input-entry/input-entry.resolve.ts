import { InputEntryService } from './input-entry.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { InputEntryStage1ViewDTO } from './input-entry.model';

@Injectable()
export class InputEntryResolve implements Resolve<InputEntryStage1ViewDTO> {
  constructor(private inputEntryService: InputEntryService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<InputEntryStage1ViewDTO> {
    return this.inputEntryService.getInputEntryListById(route.params['id']);
  }
}
