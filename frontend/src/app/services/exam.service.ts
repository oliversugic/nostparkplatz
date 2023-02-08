import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Exam } from '../models/exam.model';

@Injectable({
  providedIn: 'root'
})
export class ExamService {


  devurl = "http://localhost:5000/api/Exam/";
  constructor(private http: HttpClient) { }

  getExams() {
    return this.http.get<Exam[]>(this.devurl+"all");
  }
  addTestData(){
    return this.http.get(this.devurl+"setTestData");
  }
}
