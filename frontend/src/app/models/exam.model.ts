import { Competence } from "./competence.model"
import { Student } from "./student.model"
import { Subject } from "./subject.model"
import { Teacher } from "./teacher.model"

export interface Exam {
    id:number,
    student: Student,
    teacher: Teacher,
    subject: Subject,
    grade: number | null,
    passedExam: boolean | null,
    date: Date,
    attempt: number,
    competences: Competence[],
    description: string
}