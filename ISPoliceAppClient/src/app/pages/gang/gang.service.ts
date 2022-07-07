import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GangCreationDTO, GangDetailViewDTO, GangViewDTO } from './gang.model';

@Injectable({
  providedIn: 'root',
})
export class GangService {
  private apiURL = environment.apiUrl + '/gang';

  constructor(private http: HttpClient) {}

  getAll(): Observable<GangViewDTO[]> {
    return this.http.get<GangViewDTO[]>(this.apiURL + '/view');
  }

  getGangDetail(gangId: number): Observable<GangDetailViewDTO> {
    return this.http.get<GangDetailViewDTO>(this.apiURL + `/Detail/${gangId}`);
  }

  create(gangCreationDTO: GangCreationDTO) {
    const gangFormData = this.BuildGangFormData(gangCreationDTO);
    return this.http.post(this.apiURL, gangFormData);
  }

  private BuildGangFormData(gangCreationDTO: GangCreationDTO): FormData {
    const formData = new FormData();

    formData.append('gangName', gangCreationDTO.gangName);
    formData.append('leader', gangCreationDTO.leader);
    formData.append('activityArea', gangCreationDTO.activityArea);
    formData.append('modusOperandi', gangCreationDTO.modusOperandi);
    // let media: File[] = [];
    // if (gangCreationDTO.gangMedia.length > 0) {
    //   gangCreationDTO.gangMedia.forEach((m) => {
    //     media.push(m);
    //   });
    // }
    // formData.append('gangMedia', JSON.stringify(media));
    console.log('Gang Media Raw: ', gangCreationDTO.gangMedia);
    console.log('Gang Media Json: ', JSON.stringify(gangCreationDTO.gangMedia));
    console.log(JSON.stringify(gangCreationDTO.gangActivityIds));
    console.log(JSON.stringify(gangCreationDTO.gangMember));
    formData.append(
      'gangActivityIds',
      JSON.stringify(gangCreationDTO.gangActivityIds)
    );
    formData.append('gangMember', JSON.stringify(gangCreationDTO.gangMember));
    return formData;
  }
}
