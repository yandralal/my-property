import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Agent } from '../models/agent.model';

@Injectable({ providedIn: 'root' })
export class AgentService {
  createAgent(agent: Partial<Agent>): Observable<Agent> {
    return this.http.post<Agent>(`${environment.apiUrl}/agent`, agent);
  }

  updateAgent(id: number, agent: Partial<Agent>): Observable<Agent> {
    return this.http.put<Agent>(`${environment.apiUrl}/agent/${id}`, agent);
  }

  getAgentById(id: number): Observable<Agent> {
    return this.http.get<Agent>(`${environment.apiUrl}/agent/${id}`);
  }

  deleteAgent(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/agent/${id}`);
  }

  constructor(private http: HttpClient) {}

  getAllAgents(): Observable<Agent[]> {
    return this.http.get<Agent[]>(`${environment.apiUrl}/agent`);
  }

  getAgentTransactions(agentId: number): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5000/api/Agent/${agentId}/transactions`);
  }

  getAgentByProperty(propertyId: number): Observable<Agent> {
    return this.http.get<Agent>(`${environment.apiUrl}/agent/by-property/${propertyId}`);
  }

  getPlotsByAgentProperty(agentId: number, propertyId: number): Observable<any[]> {
    return this.http.get<any[]>(`http://localhost:5000/api/Agent/plots/by-agent-property?agentId=${agentId}&propertyId=${propertyId}`);
  }
  getBrokerageSummary(agentId: number, propertyId: number, plotId: number): Observable<any> {
    return this.http.get<any>(`http://localhost:5000/api/Agent/brokerage-summary?agentId=${agentId}&propertyId=${propertyId}&plotId=${plotId}`);
  }

  createAgentTransaction(transaction: any): Observable<any> {
    return this.http.post<any>('http://localhost:5000/api/agent/transactions', transaction);
  }

  deleteAgentTransaction(id: number): Observable<any> {
    return this.http.delete<any>(`http://localhost:5000/api/agent/transactions/${id}`);
  }
}
