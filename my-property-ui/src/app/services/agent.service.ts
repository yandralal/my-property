import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Agent } from '../models/agent.model';

@Injectable({ providedIn: 'root' })
export class AgentService {

  constructor(private http: HttpClient) {}

  getAllAgents(): Observable<Agent[]> {
    return this.http.get<Agent[]>(`${environment.apiUrl}/agent`);
  }
}
