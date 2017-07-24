import { Question, Choice, MockQuestions } from './question';

export class Tester {
    public TesterId: number;
    public Name: string;
    public IsCompleted: boolean;
    public Score: number;
    public TotalScore: number;
    public Rank: number;
    public TesterQuestions: TesterQuestion[];

    constructor(id?: number, name?: string) {
        this.TesterId = id || 0;
        this.Name = name || undefined;
    }
}

export class TesterQuestion {
    public Question: Question;
    public Choice: Choice;
}

export const MockTester = (name: string): Tester => {
    let tester: Tester = new Tester(1, name);
    let questions: Question[] = MockQuestions();

    tester.Score = 20;
    tester.TotalScore = 50;
    tester.Rank = 10;
    tester.TesterQuestions = [];

    questions.forEach(q => {
        tester.TesterQuestions.push({ Question: q, Choice: new Choice() });
    })
    tester.TesterQuestions[0].Choice = questions[0].Choices[0];
    tester.TesterQuestions[1].Choice = questions[1].Choices[2];
    
    return tester;
}