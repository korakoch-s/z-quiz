import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    public userName: string;
    public modalRef: BsModalRef;

    constructor(private router: Router, private modalService: BsModalService) { }

    ngOnInit() {
    }

    goClick(template: TemplateRef<any>) {
        if (!this.userName || this.userName.length <= 0) {
            console.log('Please input username before go...');
            this.openAlertModal(template);
            event.stopPropagation();
        } else {
            this.router.navigate(['/summary', this.userName]);
        }
    }

    public openAlertModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
    }

}
