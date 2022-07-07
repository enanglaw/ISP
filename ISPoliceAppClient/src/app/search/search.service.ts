import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {
  InputEntryPrintSearchDataDTO,
  InputEntrySearchDataDTO,
  InputEntrySearchResultDTO,
} from '../pages/input-entry/input-entry.model';
import {
  AdvancedSearchInputData,
  AdvanceSearchResultData,
} from './input-data.model';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  private ApiUrl: string;
  constructor(private http: HttpClient) {
    this.ApiUrl = environment.apiUrl;
  }

  getSearchInputData(
    data: InputEntrySearchDataDTO
  ): Observable<InputEntrySearchResultDTO[]> {
    // let searchUrl = this.ApiUrl + '/search/inputdata';
    
    let searchUrl = this.ApiUrl + '/search/inputentry';
    return this.http.post<InputEntrySearchResultDTO[]>(searchUrl, data);
  }

  advancedSearchInputData(
    data: AdvancedSearchInputData
  ): Observable<AdvanceSearchResultData[]> {
    let searchUrl = this.ApiUrl + '/search/advanced2';
    return this.http.post<AdvanceSearchResultData[]>(searchUrl, data);
  }

  searchPrintInputData(
    data: InputEntryPrintSearchDataDTO
  ): Observable<InputEntrySearchResultDTO[]> {
    let searchUrl = this.ApiUrl + '/search/print';
    return this.http.post<InputEntrySearchResultDTO[]>(searchUrl, data);
  }

  getCS(): Observable<any> {
    return this.http.get<any>('/assets/cs.json');
  }
}
