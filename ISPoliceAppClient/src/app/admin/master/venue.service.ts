import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { formatDateFormData } from 'src/app/utilities/utils';
import { environment } from 'src/environments/environment';
import {
  VenueCreationDTO,
  VenueDropdownDTO,
  VenueGridDTO,
  VenuePermissionAfterEventDetailCreationDTO,
  VenuePermissionCreationDTO,
  VenuePermissionGridDTO,
  VenuePermissionStatusCreationDTO,
  VenuePermissionStatusDTO,
  VenuePermissionStatusViewDTO,
  VenuePermissionTypeCreationDTO,
  VenuePermissionViewDTO,
} from './venue.modal';

@Injectable({
  providedIn: 'root',
})
export class VenueService {
  venueApiURL = environment.apiUrl + '/venue';
  venuePermissionApiURL = environment.apiUrl + '/venuepermission';
  venuePermissionTypeApiUrl = environment.apiUrl + '/venuepermissiontype';
  venuePermissionStatusApiURL = environment.apiUrl + '/venuepermissionstatus';

  constructor(private http: HttpClient) {}

  getVenueForDropdown(): Observable<VenueDropdownDTO[]> {
    return this.http.get<VenueDropdownDTO[]>(
      this.venueApiURL + '/VenueDropdown'
    );
  }

  getVenueForGrid(): Observable<VenueGridDTO[]> {
    return this.http.get<VenueGridDTO[]>(this.venueApiURL + '/VenueGrid');
  }

  getVenueById(id: number): Observable<VenueGridDTO> {
    return this.http.get<VenueGridDTO>(`${this.venueApiURL}/${id}`);
  }

  deleteVenue(id: number) {
    return this.http.delete(`${this.venueApiURL}/${id}`);
  }

  unDeleteVenue(id: number) {
    return this.http.delete(`${this.venueApiURL}/UnDelete/${id}`);
  }

  createVenue(venueData: VenueCreationDTO) {
    var data = this.BuildVenueFormData(venueData, 0);
    return this.http.post(this.venueApiURL, data);
  }

  editVenue(id: number, venueData: VenueCreationDTO) {
    var data = this.BuildVenueFormData(venueData, id);
    return this.http.put(`${this.venueApiURL}/${id}`, data);
  }

  getVenuePermissionTypeById(
    id: number
  ): Observable<VenuePermissionTypeCreationDTO> {
    return this.http.get<VenuePermissionTypeCreationDTO>(
      `${this.venuePermissionTypeApiUrl}/${id}`
    );
  }

  getVenuePermissionTypeForGrid(): Observable<VenuePermissionGridDTO[]> {
    return this.http.get<VenuePermissionGridDTO[]>(
      this.venuePermissionTypeApiUrl + '/Grid'
    );
  }

  createVenuePermissionType(vpTypeData: VenuePermissionTypeCreationDTO) {
    var data = this.BuildVenuePermissionTypeFormData(vpTypeData, 0);
    return this.http.post(this.venuePermissionTypeApiUrl, data);
  }

  editVenuePermissionType(
    id: number,
    vpTypeData: VenuePermissionTypeCreationDTO
  ) {
    var data = this.BuildVenuePermissionTypeFormData(vpTypeData, id);
    return this.http.put(`${this.venuePermissionTypeApiUrl}/${id}`, data);
  }

  deleteVenuePermissionType(id: number) {
    return this.http.delete(`${this.venuePermissionTypeApiUrl}/${id}`);
  }

  unDeleteVenuePermissionType(id: number) {
    return this.http.delete(`${this.venuePermissionTypeApiUrl}/UnDelete/${id}`);
  }

  getAll(): Observable<VenuePermissionViewDTO[]> {
    return this.http.get<VenuePermissionViewDTO[]>(
      this.venuePermissionApiURL + '/View'
    );
  }

  getVenuePermissionById(id: number): Observable<VenuePermissionViewDTO> {
    return this.http.get<VenuePermissionViewDTO>(
      `${this.venuePermissionApiURL}/View/${id}`
    );
  }

  create(venuePermissionData: VenuePermissionCreationDTO) {
    var data = this.BuildVenuePermissionFormData(venuePermissionData);
    return this.http.post(this.venuePermissionApiURL, data);
  }

  private BuildVenueFormData(
    venueCreationDTO: VenueCreationDTO,
    id: number
  ): FormData {
    const formData = new FormData();
    formData.append('venueId', id.toString());
    formData.append('venueName', venueCreationDTO.venueName);
    formData.append('isActive', venueCreationDTO.isActive.toString());
    return formData;
  }

  private BuildVenuePermissionTypeFormData(
    data: VenuePermissionTypeCreationDTO,
    id: number
  ): FormData {
    const formData = new FormData();
    formData.append('venuePermissionTypeId', id.toString());
    formData.append('venuePermissionTypeName', data.venuePermissionTypeName);
    formData.append('venueId', data.venueId.toString());
    formData.append('isActive', data.isActive.toString());
    return formData;
  }

  private BuildVenuePermissionFormData(
    venuePermissionCreationDTO: VenuePermissionCreationDTO
  ): FormData {
    const formData = new FormData();
    formData.append('venueId', venuePermissionCreationDTO.venueId.toString());
    formData.append('venuePermissionTypeId', venuePermissionCreationDTO.venuePermissionTypeId.toString());
    formData.append('venueDate', formatDateFormData(venuePermissionCreationDTO.venueDate));
    formData.append('organizationName', venuePermissionCreationDTO.organizationName);
    formData.append('reason', venuePermissionCreationDTO.reason);
    formData.append('expectedStrength', venuePermissionCreationDTO.expectedStrength.toString());
    formData.append('leadBy', venuePermissionCreationDTO.leadBy);
    formData.append('applicantName', venuePermissionCreationDTO.applicantName);
    formData.append('phone', venuePermissionCreationDTO.phone);
    formData.append('address', venuePermissionCreationDTO.address);
    if (venuePermissionCreationDTO.requestApplication) {
      formData.append('requestApplication', venuePermissionCreationDTO.requestApplication);
    }
    formData.append('isDeskApproved', 'true');
    formData.append('acApproved', 'false');
    formData.append('dcApproved', 'false');
    formData.append('copApproved', 'false');
    return formData;
  }

  //#region Venue Permission Status
  getVenuePermissionStatusById(
    id: number
  ): Observable<VenuePermissionStatusDTO> {
    return this.http.get<VenuePermissionStatusDTO>(
      `${this.venuePermissionStatusApiURL}/View/${id}`
    );
  }

  createVenuPermissionStatus(
    venuePermissionStatusData: VenuePermissionStatusCreationDTO
  ) {
    var data = this.BuildVenuePermissionStatusFormData(venuePermissionStatusData, 0);
    return this.http.post(`${this.venuePermissionStatusApiURL}/Create`, data);
  }

  editVenuPermissionStatus(
    venuePermissionStatusData: VenuePermissionStatusCreationDTO,
    venuePermissionStatusId: number,
    isDocumentDeleted: boolean,
  ) {
    var data = this.BuildVenuePermissionStatusFormData(
      venuePermissionStatusData, venuePermissionStatusId, isDocumentDeleted
    );
    return this.http.put(`${this.venuePermissionStatusApiURL}/Edit/${venuePermissionStatusId}`, data);
  }

  private BuildVenuePermissionStatusFormData(
    venuePermissionStatusCreationDTO: VenuePermissionStatusCreationDTO,
    venuePermissionStatusId: number,
    isDocumentDeleted: boolean | undefined = undefined,
  ): FormData {
    
    const formData = new FormData();
    formData.append('venuePermissionStatusId', venuePermissionStatusId.toString());
    formData.append('venuePermissionId', venuePermissionStatusCreationDTO.venuePermissionId.toString());
    formData.append('statusOn', formatDateFormData(venuePermissionStatusCreationDTO.statusOn));
    formData.append('statusCode', venuePermissionStatusCreationDTO.statusCode.toString());
    formData.append('statusNotes', venuePermissionStatusCreationDTO.statusNotes);
    formData.append('isDocumentDeleted', venuePermissionStatusCreationDTO.isDocumentDeleted.toString());
    if (venuePermissionStatusCreationDTO.supportDocument) {
      
      formData.append('supportDocument', venuePermissionStatusCreationDTO.supportDocument);
    }
    if (isDocumentDeleted !== undefined) {
      formData.append('isDocumentDeleted', isDocumentDeleted.toString());
    }
    console.log(formData);
    return formData;
  }
  //#endregion

  //#region VenuePermissionAfterEvent
  createVenuPermissionAfterEventDetail(
    venuePermissionAfterEventData: VenuePermissionAfterEventDetailCreationDTO, venuePermissionId: number
  ) {
    
    var data = this.BuildVenuePermissionAfterEventData(venuePermissionAfterEventData, 0);
    return this.http.put(`${this.venuePermissionApiURL}/AfterEvent/${venuePermissionId}`, data);
  }

  private BuildVenuePermissionAfterEventData(
    venuePermissionAfterEventData: VenuePermissionAfterEventDetailCreationDTO,
    venuePermissionCaseId: number,
    isDocumentDeleted: boolean | undefined = undefined
  ): FormData {
    
    const formData = new FormData();
    formData.append('venuePermissionCaseId', venuePermissionCaseId.toString());
    formData.append('venuePermissionId', venuePermissionAfterEventData.venuePermissionId.toString());
    formData.append('actualStrength', venuePermissionAfterEventData.actualStrength.toString());
    formData.append('notes', venuePermissionAfterEventData.notes);
    if (venuePermissionAfterEventData.venuePermissionCaseHistory) {
      formData.append(
        'caseHistory',
        JSON.stringify(venuePermissionAfterEventData.venuePermissionCaseHistory)
      );
    }
    console.log(formData);
    return formData;
  }
  //#endregion
}
