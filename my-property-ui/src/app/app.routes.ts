import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { PropertyFormComponent } from "./property/property-form.component";
import { AuthGuard } from './core/auth.guard';

import { AgentListComponent } from './agent/agent-list.component';
import { AgentFormComponent } from './agent/agent-form.component';
import { AgentTransactionsListComponent } from './agent/agent-transactions-list.component';
import { AgentTransactionFormComponent } from './agent/agent-transaction-form.component';
import { LoanListComponent } from './loan/loan-list.component';
import { LoanFormComponent } from './loan/loan-form.component';
import { LoanTransactionFormComponent } from './loan/loan-transaction-form.component';
import { MiscListComponent } from './misc/misc-list.component';
import { MiscFormComponent } from './misc/misc-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'property-form', component: PropertyFormComponent, canActivate: [AuthGuard] },
  { path: 'property-form/:id', component: PropertyFormComponent, canActivate: [AuthGuard] },
  { path: 'agents', component: AgentListComponent, canActivate: [AuthGuard] },
  { path: 'agents/add', component: AgentFormComponent, canActivate: [AuthGuard] },
  { path: 'agents/edit/:id', component: AgentFormComponent, canActivate: [AuthGuard] },
  { path: 'agents/transactions', component: AgentTransactionsListComponent, canActivate: [AuthGuard] },
  { path: 'agents/transactions/add', component: AgentTransactionFormComponent, canActivate: [AuthGuard] },
  { path: 'agents/transactions/edit/:id', component: AgentTransactionFormComponent, canActivate: [AuthGuard] },
  { path: 'loans', component: LoanListComponent, canActivate: [AuthGuard] },
  { path: 'loans/add', component: LoanFormComponent, canActivate: [AuthGuard] },
  { path: 'loans/edit/:id', component: LoanFormComponent, canActivate: [AuthGuard] },
  { path: 'loans/transactions/add', component: LoanTransactionFormComponent, canActivate: [AuthGuard] },
  { path: 'loans/transactions/edit/:id', component: LoanTransactionFormComponent, canActivate: [AuthGuard] },
  { path: 'misc', component: MiscListComponent, canActivate: [AuthGuard] },
  { path: 'misc/add', component: MiscFormComponent, canActivate: [AuthGuard] },
  { path: 'misc/edit/:id', component: MiscFormComponent, canActivate: [AuthGuard] }
];
