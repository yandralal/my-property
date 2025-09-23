import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PropertyService } from '../services/property.service';
import { PropertyTransactionFormComponent } from "./property-transaction-form.component";

@Component({
    selector: 'app-property-transactions-list',
    templateUrl: './property-transactions-list.component.html',
    styleUrls: ['./property-transactions-list.component.css'],
    standalone: true,
    imports: [CommonModule, PropertyTransactionFormComponent]
})
export class PropertyTransactionsListComponent implements OnInit {
    @Input() propertyId?: number;
    @Input() transactions: any[] = [];
    @Input() propertyList: { id: number; title: string; selected?: boolean; buyAmount?: number; totalLoan?: number; amountPaidTillDate?: number; balanceAmount?: number }[] = [];
    @Output() closeModal = new EventEmitter<void>();

    loading: boolean = false;
    error: string = '';

    selectedTransactionDetails: any = null;
    transactionViewMode: 'view' | 'edit' = 'view';

    constructor(private propertyService: PropertyService, private cdr: ChangeDetectorRef) { }

    ngOnInit(): void {
    }


    @Output() viewTransaction = new EventEmitter<any>();
    @Output() editTransaction = new EventEmitter<any>();

    onViewTransaction(txn: any) {
           console.log('onViewTransaction called:', txn);
           this.selectedTransactionDetails = txn;
           this.transactionViewMode = 'view';
           this.cdr.detectChanges();
           console.log('selectedTransactionDetails set:', this.selectedTransactionDetails);
    }

    onEditTransaction(txn: any) {
           console.log('onEditTransaction called:', txn);
           this.selectedTransactionDetails = txn;
           this.transactionViewMode = 'edit';
           this.cdr.detectChanges();
           console.log('selectedTransactionDetails set:', this.selectedTransactionDetails);
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
    }

    closeModalClicked() {
        this.closeModal.emit();
    }
}
