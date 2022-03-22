import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ticket } from 'src/app/models/ticket';
import { environment } from 'src/environments/environment';
import { GenericService } from '../generic/generic.service';

@Injectable({
  providedIn: 'root'
})
export class TicketsService extends GenericService {

  constructor(protected override _httpClient: HttpClient) { 
    super(_httpClient);
  }

  getTickets(): Observable<Ticket[]>{ 
    return this._httpClient.get<Ticket[]>(`${this.url}tickets`, this.httpOptions);
  }
}
