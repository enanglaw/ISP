import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { formatDateFormData } from 'src/app/utilities/utils';
import { environment } from 'src/environments/environment';
import {
  CategoryDTO,
  InputEntry1DTO,
  InputEntryStage1EditDTO,
  InputEntryStage1ViewDTO,
  InputEntryStage2CreationDTO,
  ZoneDTO,
} from './input-entry.model';

@Injectable({
  providedIn: 'root',
})
export class InputEntryService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCategory(): Observable<CategoryDTO[]> {
    return this.http.get<CategoryDTO[]>(
      this.apiUrl + '/categoryMaster/categorydropdown'
    );
  }

  getZone(): Observable<ZoneDTO[]> {
    return this.http.get<ZoneDTO[]>(this.apiUrl + '/zonemaster/zonedropdown');
  }

  getInputEntryList(): Observable<InputEntryStage1ViewDTO[]> {
    return this.http.get<InputEntryStage1ViewDTO[]>(
      this.apiUrl + '/inputentry/view'
    );
  }

  getInputEntryListById(id: number): Observable<InputEntryStage1ViewDTO> {
    return this.http.get<InputEntryStage1ViewDTO>(
      this.apiUrl + `/inputentry/view/${id}`
    );
  }

  getInputEntry1ByIdForEdit(id: number): Observable<InputEntryStage1EditDTO> {
    return this.http.get<InputEntryStage1EditDTO>(
      this.apiUrl + `/inputentry1/view/${id}`
    );
  }

  create(inputEntryData: InputEntry1DTO) {
    // console.log(inputEntryData);
    
    let data = this.BuildInputEntryFormData(inputEntryData, 0);
    return this.http.post(this.apiUrl + '/inputentry/Stage1', data);
  }

  edit(
    inputEntryData: InputEntry1DTO,
    inputEntryId: number,
    isDocumentDeleted: boolean,
    isReferenceDeleted: boolean,
    deletedReferences: number[]
  ) {
    // console.log(inputEntryData);
    let data = this.BuildInputEntryFormData(
      inputEntryData,
      inputEntryId,
      isDocumentDeleted,
      isReferenceDeleted,
      deletedReferences
    );
    return this.http.put(
      this.apiUrl + `/inputentry/Stage1/${inputEntryId}`,
      data
    );
  }

  private BuildInputEntryFormData(
    inputEntryStage1DTO: InputEntry1DTO,
    inputEntryId: number,
    isDocumentDeleted: boolean | undefined = undefined,
    isReferenceDeleted: boolean | undefined = undefined,
    deletedReferences: number[] = []
  ): FormData {
    const formData = new FormData();
    formData.append('inputEntryId', inputEntryId.toString());
    formData.append(
      'entryDate',
      formatDateFormData(inputEntryStage1DTO.entryDate)
    );
    formData.append('categoryId', inputEntryStage1DTO.categoryId.toString());
    formData.append('subCategoryId', inputEntryStage1DTO.subCategoryId.toString());
    formData.append('zoneId', inputEntryStage1DTO.zoneId.toString());
    formData.append('districtId', inputEntryStage1DTO.districtId.toString());
    formData.append('stationId', inputEntryStage1DTO.stationId.toString());
    formData.append('inputData', inputEntryStage1DTO.inputData);
    if (inputEntryStage1DTO.inputDocument) {
      console.log('inputDocument Found');
      formData.append('inputDocument', inputEntryStage1DTO.inputDocument);
    }
    if (isDocumentDeleted !== undefined) {
      formData.append('isDocumentDeleted', isDocumentDeleted.toString());
    }
    if (inputEntryStage1DTO.referenceDocument) {
      console.log('referenceDocument Found');
      formData.append('referenceDocument', inputEntryStage1DTO.referenceDocument);
    }
    if (inputEntryStage1DTO.referenceNo) {
      console.log('referenceNo Found');
      formData.append('referenceNo', inputEntryStage1DTO.referenceNo);
    }
    
    formData.append('referenceIds', JSON.stringify(inputEntryStage1DTO.referenceIds));
    if (isReferenceDeleted !== undefined) {
      formData.append('isReferenceDeleted', isReferenceDeleted.toString());
    }
    if (inputEntryId > 0) {
      formData.append('deletedReferences', JSON.stringify(deletedReferences));
    }
    return formData;
  }

  delete(inputEntryId: number) {
    return this.http.delete(this.apiUrl + `/inputentry/${inputEntryId}`);
  }

  update(
    inputEntryStage2DTO: InputEntryStage2CreationDTO,
    inputEntryId: number
  ) {
    
    console.log(inputEntryStage2DTO);
    let data = this.BuildInputEntryForm2Data(inputEntryStage2DTO, inputEntryId);
    return this.http.put(
      this.apiUrl + `/inputentry/Stage2/${inputEntryId}`,
      data
    );
  }

  private BuildInputEntryForm2Data(
    inputEntryStage2DTO: InputEntryStage2CreationDTO,
    inputEntryId: number
  ): FormData {
    const formData = new FormData();
    formData.append('inputEntryId', inputEntryId.toString());
    formData.append('personId', inputEntryStage2DTO.personId.toString());
    formData.append(
      'secondaryPersons',
      JSON.stringify(inputEntryStage2DTO.secondaryPersons)
    );
    formData.append(
      'repeatOffence',
      inputEntryStage2DTO.repeatOffence.toString()
    );
    /* formData.append(
      'offenderParentName',
      inputEntryStage2DTO.offenderParentName
    );
    formData.append('primaryAddress', inputEntryStage2DTO.primaryAddress);
    formData.append(
      'inputEntryOffenderType',
      JSON.stringify(inputEntryStage2DTO.inputEntryOffenderType)
    );
    formData.append('historySheetNo', inputEntryStage2DTO.historySheetNo);
    formData.append('currentActivity', inputEntryStage2DTO.currentActivity);
    formData.append(
      'offenderStatusId',
      inputEntryStage2DTO.offenderStatusId.toString()
    );
    formData.append('modusOperandi', inputEntryStage2DTO.modusOperandi);
    formData.append('gangId', inputEntryStage2DTO.gangId.toString());

    if (inputEntryStage2DTO.photoDocument) {
      console.log('photoDocument Found');
      formData.append('photoDocument', inputEntryStage2DTO.photoDocument);
    } */
    formData.append(
      'departmentalAlertEnglish',
      inputEntryStage2DTO.departmentalAlertEnglish
    );
    if (inputEntryStage2DTO.deptAlertEnglishDocument) {
      console.log('deptAlertEnglishDocument Found');
      formData.append(
        'deptAlertEnglishDocument',
        inputEntryStage2DTO.deptAlertEnglishDocument
      );
    }
    formData.append(
      'departmentalAlertTamil',
      inputEntryStage2DTO.departmentalAlertTamil
    );
    if (inputEntryStage2DTO.deptAlertTamilDocument) {
      console.log('deptAlertTamilDocument Found');
      formData.append(
        'deptAlertTamilDocument',
        inputEntryStage2DTO.deptAlertTamilDocument
      );
    }
    /* if (inputEntryStage2DTO.inputEntryAddress.length > 0) {
      formData.append(
        'inputEntryAddress',
        JSON.stringify(inputEntryStage2DTO.inputEntryAddress)
      );
    }
    if (inputEntryStage2DTO.inputEntryAliasName.length > 0) {
      formData.append(
        'inputEntryAliasName',
        JSON.stringify(inputEntryStage2DTO.inputEntryAliasName)
      );
    }
    if (inputEntryStage2DTO.inputEntryCaseHistory.length > 0) {
      formData.append(
        'inputEntryCaseHistory',
        JSON.stringify(inputEntryStage2DTO.inputEntryCaseHistory)
      );
    }
    if (inputEntryStage2DTO.inputEntryRivalGang.length > 0) {
      formData.append(
        'inputEntryRivalGang',
        JSON.stringify(inputEntryStage2DTO.inputEntryRivalGang)
      );
    }
    if (inputEntryStage2DTO.inputEntryMedia.length > 0) {
      inputEntryStage2DTO.inputEntryMedia.forEach((element, index) => {
        formData.append(
          `inputEntryMedia[${index}][mediaLabel]`,
          element.mediaLabel
        );
        formData.append(`inputEntryMedia[${index}][media]`, element.media);
      }); */
    /* formData.append(
        'inputEntryMedia',
        inputEntryStage2DTO.inputEntryMedia
      );
    } */

    /* if (inputEntryStage2DTO.inputEntryMedia.length > 0) {
      inputEntryStage2DTO.inputEntryMedia.forEach((media, index) {
        formData.append(`inputEntryMedia[${index}].mediaLabel`, inputEntryStage2DTO.inputEntryMedia[index].mediaLabel);
        formData.append(`inputEntryMedia[${index}].media`, inputEntryStage2DTO.inputEntryMedia[index].media)
      });
      } */
    return formData;
  }
}
