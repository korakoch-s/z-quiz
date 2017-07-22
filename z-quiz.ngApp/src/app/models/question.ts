export class Question {
    public id: number;
    public description: string;
    public choices: Choice[];
}

export class Choice {
    public id: number;
    public description: string;
    public order: number;
    public score: number;
}

export const MockQuestions = () => {
    let questions: Question[] = [];
    let chId: number = 0;

    for (let i = 0; i < 5; i++) {
        let q = new Question();
        q.id = i + 1;
        q.description = 'Question of ' + q.id;
        q.choices = [];
        for (let j = 0; j < 5; j++, chId++) {
            let ch = new Choice();
            ch.id = chId;
            ch.description = 'Answer ' + chId + ' of question ' + q.id;
            ch.score = chId % 5;
            ch.order = j;
            q.choices.push(ch);
        }
        questions.push(q);
    }

    return questions;
}
