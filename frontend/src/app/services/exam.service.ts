import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExamDTO } from '../models/exam-dto';
import { Exam } from '../models/exam.model';
import { ParkingLot } from '../models/parking-lot';


const httpOptions = {
  headers: new HttpHeaders({
  'Content-Type': 'application/json'
  //,'Authorization': 'my-auth-token'
  })
  }
@Injectable({
  providedIn: 'root'
})
export class ExamService {
  devurl = "http://localhost:5000/api/Exam/";
  constructor(private http: HttpClient) { }

  getParkingLots() {
    return this.http.get<ParkingLot[]>(this.devurl+"getParkingLots");
  }
  postExam(e:ExamDTO) {
    return this.http.post(this.devurl, e, httpOptions);
  }
  getExams() {
    return this.http.get<Exam[]>(this.devurl+"all");
  }
  addTestData(){
    return this.http.get(this.devurl+"setTestData");
  }
}
