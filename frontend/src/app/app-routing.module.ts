import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExamDetailComponent } from './components/exam-detail/exam-detail.component';
import { ListExamsComponent } from './components/list-exams/list-exams.component';
import { ListParkingLotsComponent } from './components/list-parking-lots/list-parking-lots.component';
import { RegisterExamComponent } from './components/register-exam/register-exam.component';

const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full' },
  {path: 'home', component: ListExamsComponent},
  {path: 'register', component: RegisterExamComponent},
  {path: 'details/:id', component: ExamDetailComponent},
  {path: 'parkingLots', component: ListParkingLotsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
