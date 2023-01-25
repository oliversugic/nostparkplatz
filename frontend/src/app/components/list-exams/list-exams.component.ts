import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Exam } from 'src/app/models/exam.model';

const URL = "<<Implement>>"

@Component({
  selector: 'app-list-exams',
  templateUrl: './list-exams.component.html',
  styleUrls: ['./list-exams.component.css']
})
export class ListExamsComponent implements OnInit {

  searchText:string="";

  exams:Exam[]=[]

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getExams();
  }

  getExams(){
    this.http.get(URL).subscribe((response:any) => {
      this.exams = response.data
    });
  }
}
