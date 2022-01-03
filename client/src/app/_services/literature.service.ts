import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LiteratureService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getLiterature() {
    return this.http.get(this.baseUrl + 'literature');
  }

  addLiterature(model: any) {
    return this.http.post(this.baseUrl + 'literature/add', model);    
  }
}
