import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Student } from 'src/app/models/student.model';
import { Teacher } from 'src/app/models/teacher.model';
import { StudentService } from 'src/app/services/student.service';
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

  constructor(private router: Router, 
    private teacherService:TeacherService,
    private studentService:StudentService) { }

  ngOnInit(): void {
    this.getTeachers();
    this.getStudents();
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
  onSubmit(examForm: NgForm) {
    if (examForm.valid) {
    }
  }
}
