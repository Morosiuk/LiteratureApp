import { Component, Input, OnInit } from '@angular/core';
import { CongregationCard } from 'src/app/_models/congregationCard';

@Component({
  selector: 'app-congregation-card',
  templateUrl: './congregation-card.component.html',
  styleUrls: ['./congregation-card.component.css']
})
export class CongregationCardComponent implements OnInit {
  @Input() congregation: CongregationCard;
  
  constructor() { }

  ngOnInit(): void {
  }

}
