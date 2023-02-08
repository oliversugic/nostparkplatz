import { NonNullableFormBuilder } from "@angular/forms";

export interface ExamDTO {
    studentId:number,
    teacherId:number,
    subjectId:number,
    date:Date,
    attempt:number
}
