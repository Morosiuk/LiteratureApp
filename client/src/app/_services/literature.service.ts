import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Literature } from '../_models/literature';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')).token
  })
}

@Injectable({
  providedIn: 'root'
})
export class LiteratureService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getLiterature() {
    return this.http.get(this.baseUrl + 'literature', httpOptions);
  }

  addLiterature(model: any) {
    return this.http.post(this.baseUrl + 'literature/add', model);    
  }
}
