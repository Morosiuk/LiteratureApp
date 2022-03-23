import { Component, Input, OnInit } from '@angular/core';
import { Literature } from 'src/app/_models/literature';

@Component({
  selector: 'app-literature-card',
  templateUrl: './literature-card.component.html',
  styleUrls: ['./literature-card.component.css']
})
export class LiteratureCardComponent implements OnInit {
  @Input() literature: Literature;
  
  constructor() { }

  ngOnInit(): void {
  }
}
