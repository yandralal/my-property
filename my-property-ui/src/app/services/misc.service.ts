import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class MiscService {
  constructor(private http: HttpClient) {}
  private apiUrl = environment.apiUrl + '/misctransaction';

  getAllMiscTransactions(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getMiscTransactionById(transactionId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${transactionId}`);
  }

  createMiscTransaction(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  updateMiscTransaction(transactionId: string, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${transactionId}`, data);
  }

  deleteMiscTransaction(transactionId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${transactionId}`);
  }
}
