import { Component, OnInit, Input } from '@angular/core';
import { Question } from '../../models/question';

@Component({
    selector: 'app-quiz-item',
    templateUrl: './quiz-item.component.html',
    styleUrls: ['./quiz-item.component.css']
})
export class QuizItemComponent implements OnInit {
    @Input() question: Question;
    @Input() itemNo: number;

    constructor() { }

    ngOnInit() {
    }

}
