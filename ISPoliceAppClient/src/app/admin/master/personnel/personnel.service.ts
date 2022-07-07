import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { DistrictDropdown, StationDropdownDTO } from 'src/app/shared/masterData.model';
import { environment } from 'src/environments/environment';
import {PersonnelGridViewDTO, PersonnelList,} from './personnel.model';
@Injectable({  providedIn: 'root'})
export class PersonnelService {
  private apiUrl = environment.apiUrl;
  UploadUrl = environment.apiUrl + '/Upload';
  downloadUrl = environment.apiUrl + '/Profile/downloaddocument/';


  constructor(private http: HttpClient) { }
 
  getReligionList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/Religion');;
  }
  getGenderList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/Gender');;
  }
  getMaritalStatusList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/MaritalStatus');;
  }
 
  uploadPhoto(formData): Observable<any>{
    return this.http.post(`${this.apiUrl}/Personnel/Upload`, formData, {reportProgress: true, observe: 'events'});
}

  addPersonnel(formData): any {
    console.log(formData)
    return this.http.post(this.apiUrl + '/Personnel', formData);
  }
  editPersonnel(id: number, obj: PersonnelList) {
   
    return this.http.put<PersonnelList>(`${this.apiUrl+"/Personnel"}/${id}`, obj);
  }
  getPersonnels(): Observable<PersonnelList[]> {  
    return this.http.get<PersonnelList[]>(this.apiUrl + '/Personnel');
  }

  getPersonneById(id: number): Observable<any[]> {
    var personnel = this.http.get(`${this.apiUrl+"/Personnel"}/${id}`);;
    return forkJoin([personnel]);
  }
  getPersonnelGridData(): Observable<PersonnelGridViewDTO[]> {  
    return this.http.get<PersonnelGridViewDTO[]>(this.apiUrl + '/personnel');
  }
  deletePersonnel(id: number): any {
    return this.http.delete(`${this.apiUrl+"/Personnel"}/${id}`);
  }
  downloadPersonnel( id: number) {
    return this.http.request(new HttpRequest(
      'GET', `${this.downloadUrl}${id}`,
      null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }
}
