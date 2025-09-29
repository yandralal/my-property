import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { PropertyFormComponent } from "./property/property-form.component";

import { AgentListComponent } from './agent/agent-list.component';
import { AgentFormComponent } from './agent/agent-form.component';
import { AgentTransactionsListComponent } from './agent/agent-transactions-list.component';
import { AgentTransactionFormComponent } from './agent/agent-transaction-form.component';
import { LoanListComponent } from './loan/loan-list.component';
import { LoanFormComponent } from './loan/loan-form.component';
import { LoanTransactionFormComponent } from './loan/loan-transaction-form.component';
import { MiscListComponent } from './misc/misc-list.component';
import { MiscFormComponent } from './misc/misc-form.component';
import { MiscTransactionListComponent } from './misc/misc-transaction-list.component';
import { MiscTransactionFormComponent } from './misc/misc-transaction-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'property-form', component: PropertyFormComponent },
  { path: 'property-form/:id', component: PropertyFormComponent },
  { path: 'agents', component: AgentListComponent },
  { path: 'agents/add', component: AgentFormComponent },
  { path: 'agents/edit/:id', component: AgentFormComponent },
  { path: 'agents/transactions', component: AgentTransactionsListComponent },
  { path: 'agents/transactions/add', component: AgentTransactionFormComponent },
  { path: 'agents/transactions/edit/:id', component: AgentTransactionFormComponent },
  { path: 'loans', component: LoanListComponent },
  { path: 'loans/add', component: LoanFormComponent },
  { path: 'loans/edit/:id', component: LoanFormComponent },
  { path: 'loans/transactions/add', component: LoanTransactionFormComponent },
  { path: 'loans/transactions/edit/:id', component: LoanTransactionFormComponent },
  { path: 'misc', component: MiscListComponent },
  { path: 'misc/add', component: MiscFormComponent },
  { path: 'misc/edit/:id', component: MiscFormComponent },
  { path: 'misc/transactions', component: MiscTransactionListComponent },
  { path: 'misc/transactions/add', component: MiscTransactionFormComponent },
  { path: 'misc/transactions/edit/:id', component: MiscTransactionFormComponent }
];
