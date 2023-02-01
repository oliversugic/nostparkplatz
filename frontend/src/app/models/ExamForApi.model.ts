import { Competence } from "./competence.model"
import { Student } from "./student.model"
import { Subject } from "./subject.model"
import { Teacher } from "./teacher.model"

export interface ExamForApi {
    id:string,
    studentid: string,
    teacherid: string,
    subjectid: string,
    grade: number | null,
    passedExam: boolean | null,
    date: Date,
    attempt: number
}