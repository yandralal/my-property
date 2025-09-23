import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property, RegisterPropertyRequest } from '../models/property.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PropertyService {
  
  constructor(private http: HttpClient) {}
  private apiUrl = environment.apiUrl + '/property';

  getActiveProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.apiUrl}/active`);
  }

  getPropertyById(id: number): Observable<Property> {
    return this.http.get<Property>(`${this.apiUrl}/${id}`);
  }

  registerProperty(data: RegisterPropertyRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  updateProperty(id: number, data: RegisterPropertyRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  // Transactions
  getPropertyTransactions(propertyId: number): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/property/${propertyId}/transactions`);
  }

  getActivePropertyTransactions(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/transactions/active`);
  }

  getPropertyTransactionById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/transactions/${id}`);
  }

  createPropertyTransaction(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/transactions`, data);
  }

  editPropertyTransaction(id: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/transactions/${id}`, data);
  }

  deletePropertyTransaction(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/transactions/${id}`);
  }
  getPropertyTransactionInfo(propertyId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${propertyId}/transaction-info`);
  }

  deleteProperty(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
