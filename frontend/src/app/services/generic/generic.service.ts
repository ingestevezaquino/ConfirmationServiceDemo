import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export abstract class GenericService {

  protected url: string = environment.apiUrl;
  protected httpOptions = { headers: new HttpHeaders({ 'content-type':  'application/json' }) };
  constructor(protected _httpClient: HttpClient) { }
}
