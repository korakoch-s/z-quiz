import { Injectable, Inject } from '@angular/core';
import { Tester, MockTester } from '../models/tester';
import { Question, MockQuestions } from '../models/question';

@Injectable()
export class QuizService {
    private currentTester: Tester;
    private apiUrl: string;

    constructor( @Inject('API_URL') apiUrl: string) {
        this.apiUrl = apiUrl;
        console.log('API URL: ' + apiUrl);
    }

    public register(name: string): Tester {
        //TODO: must implete real http service
        this.currentTester = MockTester(name);
        return this.currentTester;
    }

    public load(name: string): Tester {
        //TODO: must implete real http service
        if (!this.currentTester) {
            this.currentTester = MockTester(name);
        }
        return this.currentTester;
    }

    public quiz(): Question[] {
        //TODO: must implete real http service
        return MockQuestions();
    }

    public save(tester: Tester): Tester {
        //TODO: must implete real http service
        this.currentTester = tester;
        return tester;
    }

    public submit(tester: Tester): Tester {
        //TODO: must implete real http service
        let score: number = 0;
        let total: number = 0;
        tester.testItems.forEach(itm => {
            if (itm.answer) {
                score += itm.answer.score;
            }
            if (itm.question) {
                total += Math.max.apply(Math, itm.question.choices.map(ch => {
                    return ch.score;
                }));
            }
        });
        tester.score = score;
        tester.maxScore = total;
        tester.rank = 10;
        tester.submittedDate = new Date();

        this.currentTester = tester;

        return tester;
    }
}
