import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CongregationSummary } from '../_models/congregationSummary';

@Injectable({
  providedIn: 'root'
})
export class CongregationsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCongregations() {
    return this.http.get<CongregationSummary[]>(this.baseUrl + 'congregations');
  }
}
