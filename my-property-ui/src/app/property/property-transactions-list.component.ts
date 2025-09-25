import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InrFormatPipe } from '../shared/inr-format.pipe';

@Component({
    selector: 'app-property-transactions-list',
    templateUrl: './property-transactions-list.component.html',
    styleUrls: ['./property-transactions-list.component.css'],
    standalone: true,
        imports: [CommonModule, InrFormatPipe]
})
export class PropertyTransactionsListComponent {
    @Input() transactions: any[] = [];
    @Output() requestDeleteTransaction = new EventEmitter<any>();

    onDeleteTransaction(txn: any) {
        this.requestDeleteTransaction.emit(txn);
    }
}
