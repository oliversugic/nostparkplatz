import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Exam } from 'src/app/models/exam.model';
import { ExamService } from 'src/app/services/exam.service';

const URL = "<<Implement>>"

@Component({
  selector: 'app-list-exams',
  templateUrl: './list-exams.component.html',
  styleUrls: ['./list-exams.component.css']
})
export class ListExamsComponent implements OnInit {

  searchText:string="";
  exams:Exam[]=[]

  constructor(private http: HttpClient,private examservice: ExamService) { }

  ngOnInit(): void {
    this.setTestData();
    this.getExams();
  }

  getExams(){
    this.examservice.getExams().subscribe((data:Exam[]) => {
      this.exams = data
      console.log(this.exams)
    });
  }
  setTestData(){
    this.examservice.addTestData().subscribe((data:any)=>{ 
      this.getExams();
    });
    //window.location.reload();     

  }
}
