import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { PharmacyRegistrationComponent } from './pharmacy-registration/pharmacy-registration.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ManagerObjectionsComponent } from './manager-objections/manager-objections.component';



@NgModule({
  declarations: [
    AppComponent,
    BuildingsMapComponent,
    Hospital1Component,
    FrontPageComponent,
    PharmacyRegistrationComponent,
    ManagerObjectionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
