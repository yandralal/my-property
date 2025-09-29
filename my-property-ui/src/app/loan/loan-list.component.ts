import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { InrFormatPipe } from '../shared/inr-format.pipe';
import { PropertyService } from '../services/property.service';
import { ConfirmDialogComponent } from '../shared/confirm-dialog.component';
import { MessageBoxComponent } from '../shared/message-box.component';
@Component({
  selector: 'app-loan-list',
  templateUrl: './loan-list.component.html',
  styleUrls: ['./loan-list.component.css'],
  standalone: true,
  imports: [CommonModule, DatePipe, InrFormatPipe, ConfirmDialogComponent, MessageBoxComponent]
})
export class LoanListComponent {

  @Input() loans: any[] = [];
  @Output() loanEdited = new EventEmitter<any>();
  @Output() selectLoan = new EventEmitter<any>();
  @Output() editLoan = new EventEmitter<any>();
  @Output() loanDeleted = new EventEmitter<number>();
  
  selectedLoanId: number | null = null;
  confirmDeleteLoanVisible = false;
  loanToDelete: any = null;
  messageBoxVisible = false;
  messageText = '';
  showLoanFormModal = false;
  editLoanData: any = null;
  agentTransactions: any[] = [];
  loanTransactions: any[] = [];

  constructor(private propertyService: PropertyService) {}
  
  ngOnInit() {
    this.loadLoanAndSelectFirst();
  }

  private loadLoanAndSelectFirst() {
    this.propertyService.getAllPropertyLoans().subscribe({
      next: (data) => {
        this.loans = data || [];
        if (this.loans.length > 0) {
          this.selectAndFetchTransactions(this.loans[0]);
        } else {
          this.selectedLoanId = null;
          this.loanTransactions = [];
        }
      },
      error: (err) => {
        console.error('Failed to fetch loans', err);
        this.loans = [];
        this.selectedLoanId = null;
        this.loanTransactions = [];
      }
    });
  }

  onSelectLoan(loan: any) {
    this.selectAndFetchTransactions(loan);
  }

  private selectAndFetchTransactions(loan: any) {
    this.selectedLoanId = loan.id;
    this.fetchLoanTransactions(loan.id);
    this.selectLoan.emit(loan);
  }

  fetchLoanTransactions(loanId: number) {
    this.propertyService.getPropertyLoanTransactions(loanId).subscribe({
      next: (txns) => this.loanTransactions = txns || [],
      error: (err) => {
        console.error('Failed to fetch loan transactions', err);
        this.loanTransactions = [];
      }
    });
  }

  onViewLoan(loan: any) {
    this.showMessage('Viewing loan: ' + loan.id);
  }

  onEditLoan(loan: any) {
    if (!loan?.id) return;
    this.selectedLoanId = loan.id;
    this.propertyService.getLoanById(loan.id).subscribe({
      next: (data: any) => {
        this.editLoanData = data;
        this.showLoanFormModal = true;
        this.editLoan.emit(loan);
      },
      error: () => {
        this.showMessage('Failed to fetch loan details for edit.');
      }
    });
    this.showLoanFormModal = true;
    this.editLoan.emit(loan);
  }

  onDeleteLoan(loan: any) {
    this.loanToDelete = loan;
    this.confirmDeleteLoanVisible = true;
  }

  onConfirmDeleteLoan() {
    if (this.loanToDelete) {
      this.propertyService.deletePropertyLoan(this.loanToDelete.id).subscribe({
        next: () => {
          this.showMessage('Loan deleted successfully.');
          this.loanDeleted.emit(this.loanToDelete.id);
          this.confirmDeleteLoanVisible = false;
          this.loanToDelete = null;
        },
        error: (err) => {
          this.showMessage('Failed to delete loan.');
          console.error('Delete loan error:', err);
        }
      });
    }
  }

  onCancelDeleteLoan() {
    this.confirmDeleteLoanVisible = false;
    this.loanToDelete = null;
  }

  showMessage(msg: string) {
    this.messageText = msg;
    this.messageBoxVisible = true;
    setTimeout(() => {
      this.messageBoxVisible = false;
    }, 1500);
  }
}