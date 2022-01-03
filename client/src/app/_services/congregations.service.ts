import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Congregation } from '../_models/congregation';

@Injectable({
  providedIn: 'root'
})
export class CongregationsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCongregations() {
    return this.http.get<Congregation[]>(this.baseUrl + 'congregations');
  }
}
