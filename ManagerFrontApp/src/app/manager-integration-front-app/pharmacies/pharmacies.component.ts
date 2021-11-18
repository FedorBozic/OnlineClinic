import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Component({
  selector: 'app-pharmacies',
  templateUrl: './pharmacies.component.html',
  styleUrls: ['./pharmacies.component.css']
})
export class PharmaciesComponent implements OnInit {

  PharmaciesList:any =[];
  searchFilter: string = '';
  medicineName: string = '';
  medicineGrams: string = '';
  numOfBoxes: string = '';

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
    this.getPharmacies();
  }

  searchPharmaciesForMedicals(){ 
    return this.http.get<any>('http://localhost:43818/medicines/check?name=' + this.medicineName
    + "&dosage=" + this.medicineGrams + "&quantity=" + this.numOfBoxes).subscribe(data=>{
      this.PharmaciesList=data;
    });
  }

  searchPharmacies(){
    return this.http.get<any>("http://localhost:43818/Pharmacies/Filtered?searchFilter=" + this.searchFilter).subscribe(data=>{
      this.PharmaciesList=data;
    });
  }

  getPharmacies(){
    return this.http.get<any>("http://localhost:43818/Pharmacies").subscribe(data=>{
      this.PharmaciesList=data;
    });
  }
}
