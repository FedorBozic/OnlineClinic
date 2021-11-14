import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './homepage/homepage.component';


import { PatientCreateFeedbackComponent } from './patient-create-feedback/patient-create-feedback.component';
import { PatientFeedbackViewComponent } from './patient-feedback-view/patient-feedback-view.component';

const routes: Routes = [
{ path: "create-feedback", component: PatientCreateFeedbackComponent },
{ path: "view-feedback", component: PatientFeedbackViewComponent },
{ path: '', component: HomePageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }