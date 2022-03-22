import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Diagnostic } from 'src/app/models/diagnostic';
import { GenericService } from '../generic/generic.service';

@Injectable({
  providedIn: 'root'
})
export class FacilitiesService extends GenericService {

  constructor(protected override _httpClient: HttpClient) { 
    super(_httpClient);
  }

  getLastDiagnosticAndFacilities(subscriberNumber: string): Observable<Diagnostic>{
    return this._httpClient.get<Diagnostic>(`${this.url}diagnostics/${subscriberNumber}`, this.httpOptions);
  }
}
