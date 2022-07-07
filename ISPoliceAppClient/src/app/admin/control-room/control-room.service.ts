import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { ControlRoomDSRModel, ZoneDropdownDTO } from 'src/app/shared/masterData.model';
import { environment } from 'src/environments/environment';
import { ControlRoomDSR } from './control-room.model';

@Injectable({
  providedIn: 'root'
})
export class ControlRoomService {

  controlRoomApiURL = environment.apiUrl + '/ControlRoom';
  zoneURL = environment.apiUrl + '/ZoneMaster';
  CategoryMasterApiURL= environment.apiUrl + '/CategoryMaster';


  constructor(private http: HttpClient) { }
  getControlRoomGrid(): Observable<ControlRoomDSRModel[]> {
    return this.http.get<ControlRoomDSRModel[]>(this.controlRoomApiURL + '/Get');
  }
  getCategoryDrop(): Observable<any[]> {
    return this.http.get<any[]>(this.controlRoomApiURL + '/getCategoryDrop');;
  }
  // getStationDrop(): Observable<any[]> {
  //   return this.http.get<any[]>(this.controlRoomApiURL + '/StationsDropdown');;
  // }
  // getDistrictDrop(): Observable<any[]> {
  //   return this.http.get<any[]>(this.controlRoomApiURL + '/DistrictDropdown');;
  // }
  getZoneDrop(): Observable<ZoneDropdownDTO[]> {
    return this.http.get<ZoneDropdownDTO[]>(this.zoneURL + '/ZoneDropdown');;
  }
  getCategoryDropdown(){
    const zones= this.http.get<any[]>(this.controlRoomApiURL + '/zoneDrop');
    const category= this.http.get<any[]>(this.controlRoomApiURL + '/getCategoryDrop');
    const District= this.http.get<any[]>(this.controlRoomApiURL + '/DistrictDrop');
    const PoliceStation= this.http.get<any[]>(this.controlRoomApiURL + '/PoliceStationDrop');

   return forkJoin([category,zones,District,PoliceStation])

  }

  saveControlRoom(obj:ControlRoomDSR): Observable<ControlRoomDSR> {
    return this.http.post<ControlRoomDSR>(this.controlRoomApiURL + '/save', obj);

  }

  getControlRoomById(id: number): Observable<ControlRoomDSR> {
    return this.http.get<ControlRoomDSR>(`${this.controlRoomApiURL}/${id}`
    );
  }
  editControlRoom( id: number,obj: ControlRoomDSR) {
    // var data = this.BuildVenuePermissionTypeFormData(vpTypeData, id);
    return this.http.put(`${this.controlRoomApiURL}/${id}`, obj);
  }
}
