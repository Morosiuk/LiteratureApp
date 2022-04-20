import { Component, OnInit } from '@angular/core';
import { CongParams } from 'src/app/_models/Params/congParams';
import { CongregationCard } from 'src/app/_models/congregationCard';
import { Pagination } from 'src/app/_models/pagination';
import { CongregationsService } from 'src/app/_services/congregations.service';

@Component({
  selector: 'app-congregation-list',
  templateUrl: './congregation-list.component.html',
  styleUrls: ['./congregation-list.component.css']
})
export class CongregationListComponent implements OnInit {
  congregations: CongregationCard[];
  pagination: Pagination;
  congParams: CongParams;

  constructor(private congregationService: CongregationsService) { 
    this.congParams = new CongParams();
  }

  ngOnInit(): void {
    this.loadCongregations();
  }

  loadCongregations() {
    this.congregationService.getCongregations(this.congParams).subscribe(response => {
      this.congregations = response.result;
      this.pagination = response.pagination;
    });
  }

  pageChanged(event: any) {
    this.congParams.pageNumber = event.page;
    this.loadCongregations();
  }

}
