import { Question, Choice, MockQuestions } from './question';

export class Tester {
    public id: number;
    public name: string;
    public registedDate: Date;
    public submittedDate: Date;
    public score: number;
    public maxScore: number;
    public rank: number;
    public testItems: TestItem[];

    constructor(id?: number, name?: string, registedDate?: Date) {
        this.id = id || 0;
        this.name = name || undefined;
        this.registedDate = registedDate || new Date();
    }
}

export class TestItem {
    public question: Question;
    public answer: Choice;
}

export const MockTester = (name: string): Tester => {
    let tester: Tester = new Tester(1, name, new Date);
    let questions: Question[] = MockQuestions();

    tester.score = 20;
    tester.maxScore = 50;
    tester.rank = 10;
    tester.testItems = [];

    questions.forEach(q => {
        tester.testItems.push({ question: q, answer: new Choice() });
    })
    tester.testItems[0].answer = questions[0].choices[0];
    tester.testItems[1].answer = questions[1].choices[2];
    
    return tester;
}