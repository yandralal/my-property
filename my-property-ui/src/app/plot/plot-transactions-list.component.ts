import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlotService } from '../services/plot.service';
import { PlotTransactionFormComponent } from './plot-transaction-form.component';

@Component({
  selector: 'app-plot-transactions-list',
    templateUrl: './plot-transactions-list.component.html',
    styleUrls: ['./plot-transactions-list.component.css'],
  standalone: true,
    imports: [CommonModule, PlotTransactionFormComponent]
})
export class PlotTransactionsListComponent {
    @Input() propertyId?: number;
    @Input() transactions: any[] = [];
    @Input() propertyList: { id: number; title: string; selected?: boolean; buyAmount?: number; totalLoan?: number; amountPaidTillDate?: number; balanceAmount?: number }[] = [];
    @Output() closeModal = new EventEmitter<void>();

    loading: boolean = false;
    error: string = '';

    selectedTransactionDetails: any = null;
    transactionViewMode: 'view' | 'edit' = 'view';

    constructor(private plotService: PlotService) { }

    ngOnInit(): void {
        this.fetchTransactions();
    }

    fetchTransactions(): void {
        this.loading = true;
        this.error = '';
        // this.plotService.getPlotsByPropertyId().subscribe({
        //     next: (txns: any[]) => {
        //         this.transactions = txns;
        //         this.loading = false;
        //     },
        //     error: (err: any) => {
        //         this.error = 'Failed to load transactions.';
        //         this.loading = false;
        //     }
        // });
    }

    @Output() viewTransaction = new EventEmitter<any>();
    @Output() editTransaction = new EventEmitter<any>();

    onViewTransaction(txn: any) {
        this.selectedTransactionDetails = txn;
        this.transactionViewMode = 'view';
    }

    onEditTransaction(txn: any) {
        this.selectedTransactionDetails = txn;
        this.transactionViewMode = 'edit';
    }

    onDeleteTransaction(txn: any) {
        // Implement delete logic
    }

    closeTransactionFormModal() {
        this.selectedTransactionDetails = null;
    }

    onTransactionFormSuccess(event: string) {
        // Optionally refresh transactions or show message
        this.closeTransactionFormModal();
        this.fetchTransactions();
    }

    closeModalClicked() {
        this.closeModal.emit();
    }
}
