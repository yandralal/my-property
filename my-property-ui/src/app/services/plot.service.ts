import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Plot } from '../models/plot.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PlotService {
    createPlotTransaction(payload: any): Observable<any> {
        return this.http.post<any>(`${environment.apiUrl}/plot/transactions`, payload);
    }

    constructor(private http: HttpClient) { }

    // CREATE
    createPlot(plot: Plot, propertyId: number): Observable<Plot> {
        return this.http.post<Plot>(`${environment.apiUrl}/Plot`, { ...plot, propertyId });
    }
    createPlotsBulk(plots: Plot[], propertyId: number): Observable<Plot[]> {
        return this.http.post<Plot[]>(`${environment.apiUrl}/Plot/bulk`, { plots, propertyId });
    }

    // READ
    getPlotsByPropertyId(propertyId: number): Observable<Plot[]> {
        return this.http.get<Plot[]>(`${environment.apiUrl}/Property/${propertyId}/plots`);
    }
    getBasicPlotsByPropertyId(propertyId: number): Observable<Plot[]> {
        return this.http.get<Plot[]>(`${environment.apiUrl}/Property/${propertyId}/plots/basic`);
    }
    getPlotById(plotId: number): Observable<Plot> {
        return this.http.get<Plot>(`${environment.apiUrl}/Plot/${plotId}`);
    }
    // Get all plots (for dropdown)
    getAllPlots(): Observable<Plot[]> {
        return this.http.get<Plot[]>(`${environment.apiUrl}/Plot`);
    }

    // Get transaction info for a plot (similar to property transaction info)
    getPlotTransactionInfo(plotId: number): Observable<any> {
        return this.http.get<any>(`${environment.apiUrl}/Plot/${plotId}/transaction-info`);
    }

    // UPDATE
    updatePlot(plot: Plot, propertyId: number): Observable<Plot> {
        return this.http.put<Plot>(`${environment.apiUrl}/Plot/${plot.id}`, { ...plot, propertyId });
    }

    // DELETE
    bulkDeletePlots(plotIds: number[]): Observable<void> {
        return this.http.post<void>(`${environment.apiUrl}/Plot/bulk-delete`, { plotIds });
    }
    deletePlot(plotId: number, propertyId: number): Observable<void> {
        return this.http.delete<void>(`${environment.apiUrl}/Plot/${plotId}?propertyId=${propertyId}`);
    }
    deletePlotTransaction(transactionId: number): Observable<any> {
        return this.http.delete<any>(`${environment.apiUrl}/plot/transactions/${transactionId}/`);
    }

    // SALE
    addPlotSale(plotId: number, saleData: any): Observable<any> {
        return this.http.post<any>(`${environment.apiUrl}/plot/${plotId}/sale`, saleData);
    }
    editPlotSale(saleId: number, saleData: any): Observable<any> {
        return this.http.put<any>(`${environment.apiUrl}/plot/sale/${saleId}`, saleData);
    }
    deletePlotSale(saleId: number): Observable<any> {
        return this.http.delete<any>(`${environment.apiUrl}/plot/sale/${saleId}`);
    }

    // TRANSACTIONS
    getPlotTransactions(plotId: number): Observable<any[]> {
        return this.http.get<any[]>(`${environment.apiUrl}/plot/${plotId}/transactions`);
    }
}
