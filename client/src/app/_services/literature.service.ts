import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Literature } from '../_models/literature';
import { LitParams } from '../_models/Params/litParams';
import { PaginatedService } from './paginated.service';

@Injectable({
  providedIn: 'root'
})
export class LiteratureService extends PaginatedService {
  baseUrl = environment.apiUrl;

  constructor(http: HttpClient) { 
    super(http);
  }

  getLiterature(litParams: LitParams) {
    let params = this.getPaginationHeaders(litParams.pageNumber, litParams.pageSize);
    params = params.append('orderBy', litParams.orderBy);
    params = params.append('keyword', litParams.keyword);

    return this.getPaginatedResult<Literature[]>(this.baseUrl + 'literature', params);
  }

  addLiterature(model: any) {
    return this.http.post(this.baseUrl + 'literature/add', model);    
  }
}
