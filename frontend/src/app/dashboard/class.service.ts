import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ClassService {
  apiUrl = 'https://localhost:7287';

  constructor(private http: HttpClient) {}

  getAllClasses(): Observable<any> {
    return this.http.get('/api/class');
  }

  getClassById(id: number): Observable<any> {
    return this.http.get(`/api/class/${id}`);
  }

  createNewClass(newClass: any): Observable<any> {
    return this.http.post(`/api/class`, newClass);
  }

  deleteClass(id: number): Observable<any> {
    return this.http.delete(`/api/class/${id}`);
  }

  updateClass(id: number, newClass: any): Observable<any> {
    return this.http.put(`/api/class/${id}`, newClass);
  }
}
