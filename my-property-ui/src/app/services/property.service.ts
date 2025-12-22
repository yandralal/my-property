// ...existing code...
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property, RegisterPropertyRequest } from '../models/property.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PropertyService {
  constructor(private http: HttpClient) {}
  private apiUrl = environment.apiUrl + '/property';
  // Property Loan Management
  getAllPropertyLoans(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/loans`);
  }

  getLoanById(propertyLoanId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/loan/${propertyLoanId}`);
  }

  createPropertyLoan(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/loan`, data);
  }

  updatePropertyLoan(propertyLoanId: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/loan/${propertyLoanId}`, data);
  }

  deletePropertyLoan(propertyLoanId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/loan/${propertyLoanId}`);
  }

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

  // Property Loan Transactions
  getPropertyLoanTransactions(propertyLoanId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${propertyLoanId}/loan-transactions`);
  }

  createPropertyLoanTransaction(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/loan-transactions`, data);
  }

  deletePropertyLoanTransaction(transactionId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/loan-transactions/${transactionId}`);
  }

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
