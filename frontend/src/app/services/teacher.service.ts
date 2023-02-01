import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Teacher } from '../models/teacher.model';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor(private http: HttpClient) { }
  getTeacher(){
    return this.http.get<Teacher[]>("http://localhost:5000/api/Teacher/all");
  }
}
