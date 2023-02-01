import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Competence } from 'src/app/models/competence.model';
import { CompetenceService } from 'src/app/services/competence.service';

const URL = "<<Implement>>"

@Component({
  selector: 'app-exam-detail',
  templateUrl: './exam-detail.component.html',
  styleUrls: ['./exam-detail.component.css']
})
export class ExamDetailComponent implements OnInit {
  competences:Competence[]=[];

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute, private compservice: CompetenceService ) {  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params: Params) => {
        this.getDetail(params["id"])
      });
  }

  getDetail(id:string){
    this.compservice.getCompetences(id).subscribe((data:Competence[]) => {
      this.competences = data
    });
  }

}
