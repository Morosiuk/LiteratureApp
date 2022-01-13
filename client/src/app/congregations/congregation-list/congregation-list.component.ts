import { Component, OnInit } from '@angular/core';
import { CongregationSummary } from 'src/app/_models/congregationSummary';
import { CongregationsService } from 'src/app/_services/congregations.service';

@Component({
  selector: 'app-congregation-list',
  templateUrl: './congregation-list.component.html',
  styleUrls: ['./congregation-list.component.css']
})
export class CongregationListComponent implements OnInit {
  congregations: CongregationSummary[];

  constructor(private congregationService: CongregationsService) { }

  ngOnInit(): void {
    this.getCongregations();
  }

  getCongregations() {
    this.congregationService.getCongregations().subscribe(response => {
      this.congregations = response;
    })
  }

}
