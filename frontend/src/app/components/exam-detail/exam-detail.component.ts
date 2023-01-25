import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Competence } from 'src/app/models/competence.model';

const URL = "<<Implement>>"

@Component({
  selector: 'app-exam-detail',
  templateUrl: './exam-detail.component.html',
  styleUrls: ['./exam-detail.component.css']
})
export class ExamDetailComponent implements OnInit {

  searchText:string="";
  competences:Competence[]=[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getDetail();
  }

  getDetail(){
    this.http.get(URL).subscribe((response:any) => {
      this.competences = response.data
    });
  }

}
