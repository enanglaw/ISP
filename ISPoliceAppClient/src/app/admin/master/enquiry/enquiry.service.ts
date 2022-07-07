import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EnquiryList } from './enquiry.model';

@Injectable({
  providedIn: 'root'
})
export class EnquiryService {
 
  apiURL = environment.apiUrl;
  constructor(private http: HttpClient) { }

  uploadMedia(formData): Observable<any>{
    return this.http.post(`${this.apiURL}/Enquiry/Upload`, formData, {reportProgress: true, observe: 'events'});
}

  getAllegationList(): Observable<EnquiryList[]> {
    return this.http.get<EnquiryList[]>(this.apiURL + '/Enquiry');
  }
  addAllegationEnquiry(formData): any {
   
    return this.http.post(this.apiURL + '/Enquiry', formData);
  }

  getAllegationEnquiryById(id: number): Observable<any[]> {
    var enquiry = this.http.get(`${this.apiURL+"/Enquiry"}/${id}`);;
   
    return forkJoin([enquiry]);
  }

 
  editAllegationEnquiry(id: number, obj: EnquiryList) {
   
    return this.http.put<EnquiryList>(`${this.apiURL+"/Enquiry"}/${id}`, obj);
  }
  deleteAllegationEnquiry(id:number) {
    return this.http.delete(`${this.apiURL+"/Enquiry"}/${id}`)
  }
  
}