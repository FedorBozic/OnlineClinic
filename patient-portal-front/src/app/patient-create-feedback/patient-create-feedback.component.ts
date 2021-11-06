import { Component, OnInit } from '@angular/core';
import { Feedback } from 'src/feedback';
import { PatientCreateFeedbackService } from '../patient-create-feedback.service';
import {
  AUTO_STYLE,
  animate,
  state,
  style,
  transition,
  trigger
} from '@angular/animations';

const DEFAULT_DURATION = 300;

@Component({
  selector: 'app-patient-create-feedback',
  templateUrl: './patient-create-feedback.component.html',
  styleUrls: ['./patient-create-feedback.component.css'],
  animations: [
    trigger('collapse', [
      state('false', style({ height: AUTO_STYLE, visibility: AUTO_STYLE })),
      state('true', style({ height: '0', visibility: 'hidden', padding: '0' })),
      transition('false => true', animate(DEFAULT_DURATION + 'ms ease-in')),
      transition('true => false', animate(DEFAULT_DURATION + 'ms ease-out'))
    ]),
    trigger('colorState', [
      state('true', style({ background: 'linear-gradient(to right, rgba(79, 172, 254, 1), #08d)'})),
      state('false', style({ background: 'linear-gradient(to right, rgba(67, 233, 123, 1), rgba(56, 249, 215, 1))'}))
    ]),
    trigger('borderRoundingState', [
      state('true', style({ 'border-radius': '20px 20px 0px 0px'})),
      state('false', style({ 'border-radius': '20px 20px 20px 20px'})),
      transition('false => true', animate(DEFAULT_DURATION + 'ms ease-in')),
      transition('true => false', animate(DEFAULT_DURATION + 'ms ease-out'))
    ])
  ]
})
export class PatientCreateFeedbackComponent implements OnInit {
  content: string;
  isPublic: boolean;
  isAnonymous: boolean;
  feedbackSent: boolean = false;

  constructor(private _patientCreateFeedbackService: PatientCreateFeedbackService) {
    this.content = "";
    this.isPublic = false;
    this.isAnonymous = false;
  }

  ngOnInit(): void {
  }

  createFeedback() {
    if(this.contentIsValid())
    {
      let feedback = new Feedback(this.content,this.isPublic,this.isAnonymous);
      this._patientCreateFeedbackService.addFeedback(feedback).subscribe();
      this.feedbackSent = true;
    }
  }

  contentIsValid() {
    return this.content;
  }
}
