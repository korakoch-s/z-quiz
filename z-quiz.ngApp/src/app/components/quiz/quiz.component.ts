import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Question, MockQuestions, Choice } from '../../models/question';
import { Tester, TestItem, MockTester } from '../../models/tester';

@Component({
    selector: 'app-quiz',
    templateUrl: './quiz.component.html',
    styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {
    public questions: Question[];
    public tester: Tester;

    constructor(private router: Router, private activeRoute: ActivatedRoute) {
        this.questions = MockQuestions();
        this.activeRoute.params.subscribe(params => {
            this.tester = MockTester(params['username']);
        });
    }

    ngOnInit() {
    }

    submitClick() {
        console.log(JSON.stringify(this.tester));
        this.router.navigate(['/summary', this.tester.name]);
    }

    saveClick() {
        this.router.navigate(['/register']);
    }

}
