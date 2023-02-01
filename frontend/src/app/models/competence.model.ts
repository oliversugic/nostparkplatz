import { Subject } from "./subject.model"

export interface Competence {
    id:number,
    subjectId: Subject,
    description: string,
    compentences:string
}