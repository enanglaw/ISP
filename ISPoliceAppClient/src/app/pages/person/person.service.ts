import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GangDropdownDTO } from '../gang/gang.model';
import {
  CaseDTO,
  CaseStatusDTO,
  PersonStatusDTO,
  PersonTypeDTO,
} from '../input-entry/input-entry.model';
import {
  PersonCreationDTO,
  PersonDropdownDTO,
  PersonGridViewDTO,
} from './person.model';

@Injectable({
  providedIn: 'root',
})
export class PersonService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCases(): Observable<CaseDTO[]> {
    return this.http.get<CaseDTO[]>(this.apiUrl + '/CaseMaster/CaseDropdown');
  }

  getCaseStatuses(): Observable<CaseStatusDTO[]> {
    return this.http.get<CaseStatusDTO[]>(
      this.apiUrl + '/CaseStatusMaster/CaseStatusDropdown'
    );
  }

  getPersonType(): Observable<PersonTypeDTO[]> {
    return this.http.get<PersonTypeDTO[]>(
      this.apiUrl + '/PersonTypeMaster/Dropdown'
    );
  }

  getPersonStatus(): Observable<PersonStatusDTO[]> {
    return this.http.get<PersonStatusDTO[]>(
      this.apiUrl + '/PersonStatusMaster/Dropdown'
    );
  }

  getGangDropdownData(): Observable<GangDropdownDTO[]> {
    return this.http.get<GangDropdownDTO[]>(this.apiUrl + '/Gang/Dropdown');
  }

  getPersonDropdownData(): Observable<PersonDropdownDTO[]> {
    return this.http.get<PersonDropdownDTO[]>(this.apiUrl + '/person/Dropdown');
  }

  getPersonGridData(): Observable<PersonGridViewDTO[]> {
    return this.http.get<PersonGridViewDTO[]>(this.apiUrl + '/person/GridView');
  }

  create(personData: PersonCreationDTO) {
    console.log(personData);
    let data = this.BuildPersonFormData(personData);
    return this.http.post(this.apiUrl + '/person', data);
  }

  private BuildPersonFormData(personDTO: PersonCreationDTO): FormData {
    const formData = new FormData();
    formData.append('personName', personDTO.personName);
    formData.append('parentName', personDTO.parentName);
    formData.append('primaryAddress', personDTO.primaryAddress);
    formData.append('historySheetNo', personDTO.historySheetNo);
    formData.append('currentActivity', personDTO.currentActivity);
    formData.append('statusId', personDTO.statusId.toString());
    formData.append('modusOperandi', personDTO.modusOperandi);
    formData.append('gangId', personDTO.gangId.toString());
    let gMemberType =
      personDTO.gangMemberType.toString().length > 0
        ? personDTO.gangMemberType.toString()
        : '0';
    // if (formData.has('gangMemberType')) formData.delete('gangMemberType');
    formData.append('gangMemberType', gMemberType);
    if (personDTO.photoDocument) {
      console.log('photoDocument Found');
      formData.append('photoDocument', personDTO.photoDocument);
    }
    if (personDTO.personAddress.length > 0) {
      formData.append('personAddress', JSON.stringify(personDTO.personAddress));
    }
    if (personDTO.personAliasName.length > 0) {
      formData.append(
        'personAliasName',
        JSON.stringify(personDTO.personAliasName)
      );
    }
    if (personDTO.personCaseHistory.length > 0) {
      formData.append(
        'personCaseHistory',
        JSON.stringify(personDTO.personCaseHistory)
      );
    }
    if (personDTO.personPersonType.length > 0) {
      formData.append(
        'personPersonType',
        JSON.stringify(personDTO.personPersonType)
      );
    }

    if (personDTO.personRivalGang.length > 0) {
      formData.append(
        'personRivalGang',
        JSON.stringify(personDTO.personRivalGang)
      );
    }
    if (personDTO.personMedia.length > 0) {
      personDTO.personMedia.forEach((element, index) => {
        formData.append(
          `personMedia[${index}][mediaLabel]`,
          element.mediaLabel
        );
        formData.append(`personMedia[${index}][media]`, element.media);
      });
    }
    return formData;
  }
}
