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
        this.router.navigate(['/summary', this.tester.name]);
    }

    saveClick() {
        console.log(JSON.stringify(this.tester));
        this.router.navigate(['/register']);
    }

    getTesterSelect(question: Question) {
        let ans: Choice;
        let testItem: TestItem;
        if (this.tester && this.tester.testItems) {
            let testItem = this.tester.testItems.find(item => {
                return item.question.id === question.id;
            });
        }

        if (!testItem) {
            testItem = new TestItem();
            testItem.question = question;
            testItem.answer = new Choice();
            if (!this.tester.testItems) {
                this.tester.testItems = [];
            }
            this.tester.testItems.push(testItem);
        }

        ans = testItem.answer;
        return ans;
    }
}
