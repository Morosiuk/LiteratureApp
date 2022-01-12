import { Component, Input, OnInit } from '@angular/core';
import { Congregation } from 'src/app/_models/congregation';

@Component({
  selector: 'app-congregation-card',
  templateUrl: './congregation-card.component.html',
  styleUrls: ['./congregation-card.component.css']
})
export class CongregationCardComponent implements OnInit {
  @Input() congregation: Congregation;
  
  constructor() { }

  ngOnInit(): void {
  }

}
