export class Question {
    public QuestionId: number;
    public Title: string;
    public TotalScore: number;
    public Choices: Choice[];
}

export class Choice {
    public ChoiceId: number;
    public Title: string;
}

export const MockQuestions = () => {
    let questions: Question[] = [];
    let chId: number = 0;

    for (let i = 0; i < 5; i++) {
        let q = new Question();
        q.QuestionId = i + 1;
        q.Title = 'Question of ' + q.QuestionId;
        q.Choices = [];
        for (let j = 0; j < 5; j++, chId++) {
            let ch = new Choice();
            ch.ChoiceId = chId;
            ch.Title = 'Answer ' + chId + ' of question ' + q.QuestionId;
            q.Choices.push(ch);
        }
        questions.push(q);
    }

    return questions;
}
