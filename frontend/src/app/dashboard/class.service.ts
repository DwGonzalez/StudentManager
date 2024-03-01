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

  getStudentsClass(id: number): Observable<any> {
    return this.http.get(`/api/class/${id}/students`);
  }

  assignScoreToStudent(id: number, request: any): Observable<any> {
    return this.http.put(`/api/class/${id}/assign-score`, request);
  }

  getAttendanceList(classId: number): Observable<any> {
    return this.http.get(`/api/class/${classId}/get-attendance-list`);
  }

  removeStudentFromClass(classId: number, studentId: number): Observable<any> {
    return this.http.delete(
      `/api/class/${classId}/student-from-class?studentId=${studentId}`
    );
  }

  getStudentsNotInClass(classId: number): Observable<any> {
    return this.http.get(`/api/class/${classId}/students-not-in-class`);
  }

  addStudentsToClass(classId: number, request: any): Observable<any> {
    return this.http.post(`/api/class/${classId}/addToClass`, request);
  }
}
