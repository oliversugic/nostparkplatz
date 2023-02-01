import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Competence } from '../models/competence.model';

@Injectable({
  providedIn: 'root'
})
export class CompetenceService {


  constructor(private http: HttpClient) { }

  getCompetences(id: string) {
    console.log(id);
    return this.http.get<Competence[]>(`http://localhost:5000/api/Competence/getCompetenceForSubject/`+id);
  }

}
