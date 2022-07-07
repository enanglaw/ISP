import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LeadersDTO, LeadersList, LeadersModel } from './leaders.model';

@Injectable({
  providedIn: 'root'
})
export class LeadersService {

  leadersApiURL = environment.apiUrl ;


  constructor(private http: HttpClient) { }
  getLeadersGrid(): Observable<LeadersDTO[]> {
    return this.http.get<LeadersDTO[]>(this.leadersApiURL + '/Leaders');
  }
  
  uploadUrl(formData): any {
    return this.http.post(this.leadersApiURL + '/LeadersGroup/Upload', formData,{reportProgress: true, observe: 'events'});
  }
 
  addLeaders(formData): any {
    console.log(formData)
     return this.http.post(this.leadersApiURL + '/LeadersGroup/PostOrganizationLeaders', formData);
  }
 
  getLeadersById(id: number): Observable<any[]> {
    var leader = this.http.get(`${this.leadersApiURL+"/LeadersGroup/GetLeader"}/${id}`);;
   
    return forkJoin([leader]);
  }
  getOrganizationDropdown(): Observable<any[]> {
    return this.http.get<any[]>(this.leadersApiURL + '/Organization/SubOrganizationDropdown');;
  }
  getReligionList(): Observable<any[]> {
    return this.http.get<any[]>(this.leadersApiURL + '/Religion');;
  }
  getGenderList(): Observable<any[]> {
    return this.http.get<any[]>(this.leadersApiURL + '/Gender');;
  }
  getMaritalStatusList(): Observable<any[]> {
    return this.http.get<any[]>(this.leadersApiURL + '/MaritalStatus');;
  }
  editLeaders( id: number,obj: LeadersList) {
    return this.http.put(`${this.leadersApiURL+"/LeadersGroup"}/${id}`, obj);
  }
  deleteLeaders(id:number) {
    return this.http.delete(`${this.leadersApiURL+"/LeadersGroup"}/${id}`)
  }
  getSubOrganizationLeaderDataById(id:number): Observable<LeadersModel[]> {  
    return this.http.get<LeadersModel[]>(`${this.leadersApiURL}/SubOrganizationLeaders/${id}`);
  }

  getOrganizationLeaderDataById(id:number): Observable<LeadersModel[]> { 
    return this.http.get<LeadersModel[]>(`${this.leadersApiURL}/LeadersGroup/${id}` );
  }
  getAllLeaderShip(): Observable<LeadersModel[]> { 
    return this.http.get<LeadersModel[]>(`${this.leadersApiURL}/LeadersGroup` );
  }
   
  
  getOrganizationDataById(id: number): Observable<any[]> {
    var organization = this.http.get(`${this.leadersApiURL+"/Organization"}/${id}`);;
   
    return forkJoin([organization]);
  }
}