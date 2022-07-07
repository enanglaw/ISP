import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { stringify } from 'querystring';
import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PlanList } from './plan.model';

@Injectable({
  providedIn: 'root'
})
export class PlanService {

  planApiURL = environment.apiUrl;
  downloadUrl = environment.apiUrl + '/Plan/downloaddocument/';


  constructor(private http: HttpClient) { }
  getplanGrid(): Observable<PlanList[]> {
    return this.http.get<PlanList[]>(this.planApiURL + '/Plan/Planview');
  }
 
  uploadUrl(formData): any {
    return this.http.post(this.planApiURL + '/Plan/Upload', formData,{reportProgress: true, observe: 'events'});
  }
 

  addPlan(formData):Observable<PlanList[]> {
    return this.http.post<PlanList[]>(this.planApiURL + '/Plan', formData );
  }

  getPlanById(id: number): Observable<any[]> {
    var plan = this.http.get(this.planApiURL + '/'+id);
    return forkJoin([plan]);
  }

  deletePlan(id: number): any {
    return this.http.delete(`${this.planApiURL}/plan/${id}`);
  }
  authenticate(password: string): boolean {
    const validatorKey="Test@123";
   
    return (password===validatorKey)? true:false
  }
  downloadPlan( id: number) {
    return this.http.request(new HttpRequest(
      'GET', `${this.planApiURL}/Plan/${id}`,
      null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }
}
