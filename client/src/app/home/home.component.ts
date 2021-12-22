import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
   registerMode:boolean=false;
   users:any;
  constructor(){ }

  ngOnInit(): void {
  }

  registerToggle(){
    this.registerMode=true;
  }

  cancleRegisterMode(event:boolean){
    this.registerMode = event;
  }

}