import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Question, MockQuestions } from '../../models/question';

@Component({
    selector: 'app-quiz',
    templateUrl: './quiz.component.html',
    styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {
    public questions: Question[];
    public userName: string;

    constructor(private router: Router, private activeRoute: ActivatedRoute) {
        this.questions = MockQuestions;
        this.activeRoute.params.subscribe(params => {
            this.userName = params['username'];
        });
    }

    ngOnInit() {
    }

    submitClick() {
        this.router.navigate(['/summary', this.userName]);
    }

    saveClick() {
        this.router.navigate(['/register']);
    }

}
