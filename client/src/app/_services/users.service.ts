import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();

  constructor(private http: HttpClient) { }

  getUser(username: string) {
    return this.http.get<User>(this.baseUrl + 'users/' + username);
  }

  getUsers(userParams: UserParams) {
    let params = this.getPaginationHeaders(
      userParams.pageNumber, 
      userParams.pageSize,
      userParams.congregation);

    return this.getPaginatedResult<User[]>(this.baseUrl + 'users', params);
  }

  private getPaginatedResult<T>(url, params: HttpParams) {
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

  private getPaginationHeaders(pageNumber: number, pageSize: number, congregation: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('congregation', congregation.toString());

    return params;
  }
  
}
