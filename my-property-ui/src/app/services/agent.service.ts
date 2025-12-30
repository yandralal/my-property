import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Agent } from '../models/agent.model';

@Injectable({ providedIn: 'root' })
export class AgentService {
  createAgent(agent: Partial<Agent>): Observable<Agent> {
    return this.http.post<Agent>(`${environment.apiUrl}/api/Agent`, agent);
  }

  updateAgent(id: number, agent: Partial<Agent>): Observable<Agent> {
    return this.http.put<Agent>(`${environment.apiUrl}/api/Agent/${id}`, agent);
  }

  getAgentById(id: number): Observable<Agent> {
    return this.http.get<Agent>(`${environment.apiUrl}/api/Agent/${id}`);
  }

  deleteAgent(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/api/Agent/${id}`);
  }

  constructor(private http: HttpClient) {}

  getAllAgents(): Observable<Agent[]> {
    return this.http.get<Agent[]>(`${environment.apiUrl}/api/Agent`);
  }

  getAgentTransactions(agentId: number): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/api/Agent/${agentId}/transactions`);
  }

  getAgentByProperty(propertyId: number): Observable<Agent> {
    return this.http.get<Agent>(`${environment.apiUrl}/api/Agent/by-property/${propertyId}`);
  }

  getPlotsByAgentProperty(agentId: number, propertyId: number): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/api/Agent/plots/by-agent-property?agentId=${agentId}&propertyId=${propertyId}`);
  }
  getBrokerageSummary(agentId: number, propertyId: number, plotId: number): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/api/Agent/brokerage-summary?agentId=${agentId}&propertyId=${propertyId}&plotId=${plotId}`);
  }

  createAgentTransaction(transaction: any): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/api/Agent/transactions`, transaction);
  }

  deleteAgentTransaction(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/api/Agent/transactions/${id}`);
  }
}
