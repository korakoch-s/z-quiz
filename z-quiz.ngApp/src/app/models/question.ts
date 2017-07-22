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

export const MockQuestions: Question[] = [
    {
        id: 1, description: 'Question 1', choices: [
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
        ]
    },
    {
        id: 2, description: 'Question 1', choices: [
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
        ]
    },
    {
        id: 3, description: 'Question 1', choices: [
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
        ]
    },
    {
        id: 4, description: 'Question 1', choices: [
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
        ]
    },
    {
        id: 5, description: 'Question 1', choices: [
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
            { id: 1, description: 'choice 1 of question 1', order: 1, score: 2 },
        ]
    },
];