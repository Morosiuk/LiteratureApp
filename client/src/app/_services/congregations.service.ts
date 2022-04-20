import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CongParams } from '../_models/Params/congParams';
import { Congregation } from '../_models/congregation';
import { CongregationCard } from '../_models/congregationCard';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { PaginatedService } from './paginated.service';

@Injectable({
  providedIn: 'root'
})
export class CongregationsService extends PaginatedService {
  baseUrl = environment.apiUrl;
  currentUser: User;
  
  constructor(
    http: HttpClient,
    private accountService: AccountService) { 
    super(http);
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.currentUser = user;
    });
  }

  getCongregation(name: string) {
    return this.http.get<Congregation>(this.baseUrl + 'congregations/' + name);
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
