import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Question, MockQuestions, Choice } from '../../models/question';
import { Tester, TesterQuestion, MockTester } from '../../models/tester';
import { QuizService } from '../../services/quiz.service';

@Component({
    selector: 'app-quiz',
    templateUrl: './quiz.component.html',
    styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {
    public questions: Question[];
    public tester: Tester;

    constructor(private router: Router, private activeRoute: ActivatedRoute,
        private quizSvr: QuizService) {
        this.questions = MockQuestions();
        this.activeRoute.params.subscribe(params => {
            this.tester = this.quizSvr.load(params['username']);
        });
    }

    ngOnInit() {
    }

    submitClick() {
        this.quizSvr.submit(this.tester);
        console.log(JSON.stringify(this.tester));
        this.router.navigate(['/summary', this.tester.Name]);
    }

    saveClick() {
        this.quizSvr.save(this.tester);
        this.router.navigate(['/register']);
    }

}
