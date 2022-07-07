import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MemorandumList, NotesList } from './report.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  apiURL = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getNotes(): Observable<NotesList[]> {
    return this.http.get<NotesList[]>(this.apiURL + '/Allegation');
  }
  addNotes(formData): any {
   
    return this.http.post(this.apiURL + '/AllegationNote', formData);
  }
  getNoteList(): Observable<NotesList[]> {
      return this.http.get<NotesList[]>(this.apiURL + '/AllegationNote');
    }
    
 
  getNotesById(id: number): Observable<any[]> {
    var enquiry = this.http.get(`${this.apiURL+"/AllegationNote"}/${id}`);;
       return forkJoin([enquiry]);
  }

 
  editNotes(id: number, obj: NotesList) {
   
    return this.http.put<NotesList>(`${this.apiURL+"/AllegationNote"}/${id}`, obj);
  }
  deleteNotes(id:number) {
    return this.http.delete(`${this.apiURL+"/AllegationNote"}/${id}`)
  }
  getMemorandumList(): Observable<MemorandumList[]> {
    return this.http.get<MemorandumList[]>(this.apiURL + '/Memorandum');
  }

  getMemorandum(): Observable<MemorandumList[]> {
    return this.http.get<MemorandumList[]>(this.apiURL + "/Allegation");
  }
  addMemorandum(formData): any {
   
    return this.http.post(this.apiURL + "/Memorandum", formData);
  }

  getMemorandumById(id: number): Observable<any[]> {
    var enquiry = this.http.get(`${this.apiURL+"/Memorandum"}/${id}`);;
   
    return forkJoin([enquiry]);
  }

 
  editMemorandum(id: number, obj: MemorandumList) {
   
    return this.http.put<MemorandumList>(`${this.apiURL+"/Memorandum"}/${id}`, obj);
  }
  deleteMemorandum(id:number) {
    return this.http.delete(`${this.apiURL+"/Memorandum"}/${id}`)
  }
  
}