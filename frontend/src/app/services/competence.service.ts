import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CompetenceDTO } from '../models/competence-dto';
import { Competence } from '../models/competence.model';

@Injectable({
  providedIn: 'root'
})
export class CompetenceService {


  constructor(private http: HttpClient) { }

  getCompetences(id: string) {  
    return this.http.get<CompetenceDTO[]>(`http://localhost:5000/api/Competence/getCompetenceForSubject?subjectId=`+id);
  }

}
