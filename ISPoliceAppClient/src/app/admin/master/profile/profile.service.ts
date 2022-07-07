import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { DistrictDropdown, StationDropdownDTO } from 'src/app/shared/masterData.model';
import { environment } from 'src/environments/environment';
import { ProfileAssociates, ProfileList } from './profile.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private cityJson = 'assets/citylist.json';
  profileApiURL = environment.apiUrl + '/Profile';
  districtApiURL = environment.apiUrl + '/DistrictMaster/AllDistrictDropdown';
  stationApiURL = environment.apiUrl + '/StationMaster/AllStationsDropdown';

  zoneURL = environment.apiUrl + '/ZoneMaster';
  CategoryMasterApiURL= environment.apiUrl + '/CategoryMaster';
  UploadUrl = environment.apiUrl + '/Upload';
  downloadUrl = environment.apiUrl + '/Profile/downloaddocument/';


  constructor(private http: HttpClient) { }
  getprofileGrid(): Observable<ProfileList[]> {
    return this.http.get<ProfileList[]>(this.profileApiURL + '/profileList');
  }
  getDistrictList(): Observable<DistrictDropdown[]> {
    return this.http.get<DistrictDropdown[]>(this.districtApiURL);
  }
  getaliceList(): Observable<DistrictDropdown[]> {
    return this.http.get<DistrictDropdown[]>(this.districtApiURL);
  }
  uploadPhoto(formData): any {
    return this.http.post(this.UploadUrl, formData, {reportProgress: true, observe: 'events'});
  }
  getCityList(): Observable<any> {
    return this.http.get(this.cityJson);
  }

  getPSList(): Observable<StationDropdownDTO[]> {
    return this.http.get<StationDropdownDTO[]>(this.stationApiURL);
  }

  getCategoryDrop(): Observable<any[]> {
    return this.http.get<any[]>(this.profileApiURL + '/getCategoryDrop');;
  }

  getGetAssociates(name): Observable<any[]> {
    return this.http.get<any[]>(this.profileApiURL + '/GetAssociates?name='+name);;
  }

  addQuickProfile(formData): any {
    return this.http.post(this.profileApiURL + '/addQuickProfile', formData);
  }

  addProfile(formData): any {
    return this.http.post(this.profileApiURL + '/save', formData);
  }

  getProfileId(id: number): Observable<any[]> {
    var profile = this.http.get(this.profileApiURL + '/'+id);;
    var associateList= this.http.get(this.profileApiURL + '/getAssociateList/'+id);
    return forkJoin([profile, associateList]);
  }

  getAssociateList(id: number): any {
    
  }
  deleteAssociate(id: number): any {
    return this.http.delete(`${this.profileApiURL}/${id}`);
  }

  editProfile( id: number,obj: ProfileList) {
    return this.http.put(`${this.profileApiURL}/${id}`, obj);
  }

  downloadProfile( id: number) {
    return this.http.request(new HttpRequest(
      'GET', `${this.downloadUrl}${id}`,
      null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }
}
