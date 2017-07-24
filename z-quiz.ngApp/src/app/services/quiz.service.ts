import { Injectable, Inject } from '@angular/core';
import { Tester, MockTester } from '../models/tester';
import { Question, MockQuestions } from '../models/question';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class QuizService {
    private currentTester: Tester;
    private apiUrl: string;

    constructor( @Inject('API_URL') apiUrl: string, private http: HttpClient) {
        this.apiUrl = apiUrl;
        console.log('API URL: ' + apiUrl);
    }

    public register(name: string): Tester {
        //TODO: must implete real http service

        this.http.get(`${this.apiUrl}register/${name}`).subscribe(result => {
            console.log(JSON.stringify(result));
        });

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

        //let score: number = 0;
        //let total: number = 0;
        //tester.TesterQuestions.forEach(itm => {
        //    if (itm.Choice) {
        //        score += itm.answer.score;
        //    }
        //    if (itm.question) {
        //        total += Math.max.apply(Math, itm.question.choices.map(ch => {
        //            return ch.score;
        //        }));
        //    }
        //});
        tester.Score = 10;
        tester.TotalScore = 50;
        tester.Rank = 10;
        tester.IsCompleted = true;

        this.currentTester = tester;

        return tester;
    }
}
