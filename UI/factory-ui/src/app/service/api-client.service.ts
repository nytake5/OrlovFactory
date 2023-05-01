import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry } from 'rxjs';
import { Employee } from '../types/Employee';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {

  constructor(private httpClient: HttpClient) { }

  getAllEmployees() : Observable<Employee[]> {
    return this.httpClient.get<Employee[]>(
      'http://localhost:5001/api/HrDepartment'
    ).pipe(
      retry(2)
    );
  }
}
