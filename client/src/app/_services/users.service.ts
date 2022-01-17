import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { PaginatedService } from './paginated.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService extends PaginatedService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
  userCache = new Map();
  currentUser: User;
  userParams: UserParams;

  constructor(
    http: HttpClient, 
    private accountService: AccountService) { 
      super(http);
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
        this.currentUser = user;
        this.userParams = new UserParams();
        this.userParams.congregation = this.currentUser.congregationId;
      });
    }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params : UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = new UserParams();
    return this.userParams;
  }

  getUser(username: string) {

    //flatten paginated cache into a single array to find the user
    const cachedUser = [...this.userCache.values()]
      .reduce((arr, elements) => arr.concat(elements.results), [])
      .find((u : User) => u?.username === username);

    if (cachedUser) {
      return of(cachedUser);
    }

    return this.http.get<User>(this.baseUrl + 'users/' + username);
  }

  getUsers(userParams: UserParams) {

    var cachedValues = this.userCache.get(this.generateCacheKey(userParams))
    if (cachedValues) {
      return of(cachedValues);
    }

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('congregation', userParams.congregation.toString());
    params = params.append('orderBy', userParams.orderBy);

    return this.getPaginatedResult<User[]>(this.baseUrl + 'users', params).pipe(
      map(response => {
        var key = 
        this.userCache.set(this.generateCacheKey(userParams), response);
        return response;
      })
    );
  }

  ///Generate a key of the filter values provided in the userparams
  private generateCacheKey(userParams: UserParams): string {
    return Object.values(userParams).join('.');
  }
  
}
