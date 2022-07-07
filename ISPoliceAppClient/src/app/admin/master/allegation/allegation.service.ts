import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AllegationList } from './allegation.model';

@Injectable({
  providedIn: 'root'
})
export class AllegationService {
  private allegationApiURL = environment.apiUrl;
 


  constructor(private http: HttpClient) { }
  
  uploadMedia(formData): Observable<any>{
        return this.http.post(`${this.allegationApiURL}/Allegation/Upload`, formData, {reportProgress: true, observe: 'events'});
  }
  

  addAllegation(formData): any {
   
    return this.http.post(this.allegationApiURL + '/Allegation', formData);
  }
  editAllegation(id: number, obj: AllegationList) {
   
    return this.http.put<AllegationList>(`${this.allegationApiURL+"/Allegation"}/${id}`, obj);
  }
  getAllegations(): Observable<AllegationList[]> {  
    return this.http.get<AllegationList[]>(this.allegationApiURL + '/Allegation');
  }
  getAllegationById(id: number): Observable<any[]> {
    var organization = this.http.get(`${this.allegationApiURL+"/Allegation"}/${id}`);;
   
    return forkJoin([organization]);
  }  
  deleteAllegation(id:number) {
    return this.http.delete(`${this.allegationApiURL+"/Allegation"}/${id}`)
  }
  
  
 
}
