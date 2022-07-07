import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { workflowCreationDTO, workflowDTO } from './workflow.models';

@Injectable({
  providedIn: 'root',
})
export class WorkflowService {
  private apiURL = environment.apiUrl + '/workflowmaster';

  constructor(private http: HttpClient) {}

  getAll(): Observable<workflowDTO[]> {
    return this.http.get<workflowDTO[]>(this.apiURL);
  }

  getById(id: number): Observable<workflowDTO> {
    return this.http.get<workflowDTO>(`${this.apiURL}/${id}`);
  }

  create(workflow: workflowCreationDTO) {
    workflow.isActive = true;
    console.log(workflow);
    return this.http.post(this.apiURL, workflow);
  }

  edit(id: number, workflow: workflowCreationDTO) {
    return this.http.put(`${this.apiURL}/${id}`, workflow);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
}
