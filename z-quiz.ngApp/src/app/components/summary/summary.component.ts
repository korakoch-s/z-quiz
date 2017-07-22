import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Tester } from '../../models/tester';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {
    public tester: Tester;

    constructor(private activeRoute: ActivatedRoute) {
        this.activeRoute.params.subscribe(params => {
            this.tester = new Tester(0, params['username'], new Date());
            this.tester.score = 30;
            this.tester.maxScore = 50;
            this.tester.rank = 15;
        });
    }

    ngOnInit() { }

}
