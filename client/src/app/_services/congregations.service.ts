import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CongParams } from '../_models/congParams';
import { CongregationCard } from '../_models/congregationCard';
import { PaginatedService } from './paginated.service';

@Injectable({
  providedIn: 'root'
})
export class CongregationsService extends PaginatedService {
  baseUrl = environment.apiUrl;
  
  constructor(http: HttpClient) { 
    super(http);
  }

  getCongregations(congParams: CongParams) {
    let params = this.getPaginationHeaders(congParams.pageNumber, congParams.pageSize);
    params = params.append('orderBy', congParams.orderBy);
    params = params.append('keyword', congParams.keyword);
    
    return this.getPaginatedResult<CongregationCard[]>(this.baseUrl + 'congregations', params);
  }

  addCongregation(congregation: any) {
    return this.http.post(this.baseUrl + 'congregations/add', congregation);
  }
}
