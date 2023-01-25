import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ExamDetailComponent } from './components/exam-detail/exam-detail.component';
import { ListExamsComponent } from './components/list-exams/list-exams.component'
import { RegisterExamComponent } from './components/register-exam/register-exam.component';

import { SearchFilterPipe } from "./../search-filter.pipe";

@NgModule({
  declarations: [
    AppComponent,
    RegisterExamComponent,
    ExamDetailComponent,
    ListExamsComponent,
    SearchFilterPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
