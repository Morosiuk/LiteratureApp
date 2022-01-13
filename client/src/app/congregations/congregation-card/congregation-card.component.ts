import { Component, Input, OnInit } from '@angular/core';
import { CongregationSummary } from 'src/app/_models/congregationSummary';

@Component({
  selector: 'app-congregation-card',
  templateUrl: './congregation-card.component.html',
  styleUrls: ['./congregation-card.component.css']
})
export class CongregationCardComponent implements OnInit {
  @Input() congregation: CongregationSummary;
  
  constructor() { }

  ngOnInit(): void {
  }

}
