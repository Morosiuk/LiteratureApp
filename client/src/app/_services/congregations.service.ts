import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CongregationCard } from '../_models/congregationCard';

@Injectable({
  providedIn: 'root'
})
export class CongregationsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCongregations() {
    return this.http.get<CongregationCard[]>(this.baseUrl + 'congregations');
  }

  addCongregation(model: any) {
    return this.http.post(this.baseUrl + 'congregations', model);
  }
}
