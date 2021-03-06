import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class PaginatedService {

    constructor(protected http: HttpClient) {    
    }
  
    /**
     * Get the results of T as a paginated response.
     * @param url The api url
     * @param params the userparams containing the page information
     * @returns 
     */
     getPaginatedResult<T>(url: string, params: HttpParams) {
      const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
  
      return this.http.get<T>(url, { observe: 'response', params }).pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
    }
  
    getPaginationHeaders(pageNumber: number, pageSize: number) {
      let params = new HttpParams();
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());
  
      return params;
    }
}
