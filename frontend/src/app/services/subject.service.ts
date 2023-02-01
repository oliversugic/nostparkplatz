import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from '../models/subject.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private http: HttpClient) { }

  getSubjectById(id:String){
    return this.http.get<Subject>(`http://localhost:5000/api/Subject?id=`+id);
  }
}
