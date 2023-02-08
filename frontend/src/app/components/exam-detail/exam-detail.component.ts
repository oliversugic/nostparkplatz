import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CompetenceDTO } from 'src/app/models/competence-dto';
import { Competence } from 'src/app/models/competence.model';
import { Subject } from 'src/app/models/subject.model';
import { CompetenceService } from 'src/app/services/competence.service';
import { SubjectService } from 'src/app/services/subject.service';

const URL = "<<Implement>>"

@Component({
  selector: 'app-exam-detail',
  templateUrl: './exam-detail.component.html',
  styleUrls: ['./exam-detail.component.css']
})
export class ExamDetailComponent implements OnInit {
  competences:Competence[]=[];
  competenceDtos:CompetenceDTO[]=[];
  subjectToAdd :Subject={
    id: 0,
    name: ''
  };

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute,
     private compservice: CompetenceService, private subjectService:SubjectService ) {  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      (params: Params) => {
        this.getDetail(params["id"])
      });
  }

  getDetail(id:string){
    this.compservice.getCompetences(id).subscribe((data:CompetenceDTO[]) => {
      this.competenceDtos = data;
      this.competenceDtos.forEach(l=>{
        const competenceToAdd:Competence={
          id: 0,
          subjectId: {
            id: 0,
            name: ''
          },
          description: '',
          compentences: ''
        };
        competenceToAdd.compentences = l.compentences;
        competenceToAdd.description = l.descripton;
        this.subjectService.getSubjectById(l.subjectId).subscribe((data:Subject)=>{
          competenceToAdd.subjectId = data;
        });
        this.competences.push(competenceToAdd);
      });
    });
  }
}

