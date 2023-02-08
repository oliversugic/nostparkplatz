import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Exam } from 'src/app/models/exam.model';
import { Student } from 'src/app/models/student.model';
import { Subject } from 'src/app/models/subject.model';
import { Teacher } from 'src/app/models/teacher.model';
import { ExamService } from 'src/app/services/exam.service';
import { StudentService } from 'src/app/services/student.service';
import { SubjectService } from 'src/app/services/subject.service';
import { TeacherService } from 'src/app/services/teacher.service';

@Component({
  selector: 'app-register-exam',
  templateUrl: './register-exam.component.html',
  styleUrls: ['./register-exam.component.css']
})
export class RegisterExamComponent implements OnInit {
  teachers: Teacher[]=[];
  teacherAdd:Teacher={
    id: 0,
    firstName: '',
    lastName: ''
  };
  students:Student[]=[];
  studentAdd:Student={
    id: 0,
    firstName: '',
    lastName: ''
  };
  subjectAdd:Subject={
    id:0,
    name: ''
  };
  attemptNr:any
  subjects:Subject[]=[];
  dateOfExam:Date= new Date(); //TODO: Implement


  constructor(private router: Router, 
    private teacherService:TeacherService,
    private studentService:StudentService,
    private subjectService:SubjectService,
    private examservice:ExamService) { }

  ngOnInit(): void {
    this.getTeachers();
    this.getStudents();
    this.getSubjects();
  }
  getStudents() {
    this.studentService.getStudents().subscribe((data:Student[]) => {
      this.students = data
    });
  }
  getTeachers() {
    this.teacherService.getTeacher().subscribe((data:Teacher[]) => {
      this.teachers = data
    });
  }
  getSubjects() {
    this.subjectService.getSubjects().subscribe((data:Subject[]) => {
      this.subjects = data;
    })
  }
  onSubmit(examForm: NgForm) {
    if (examForm.valid) {
      this.examservice.postExam(this.studentAdd, this.teacherAdd, this.subjectAdd, this.dateOfExam, this.attemptNr);
    }
  }
}
