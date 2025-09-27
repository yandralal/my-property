export interface AgentTransaction {
  transactionId: number;
  agentId: number;
  plotId: number;
  propertyId: number;
  transactionDate: Date;
  amount: number;
  paymentMethod: string;
  referenceNumber: string;
  transactionType: string;
  notes: string;
  createdBy: string;
  createdDate?: Date;
  modifiedBy: string;
  modifiedDate?: Date;
  isDeleted: boolean;
  agentName: string;
  propertyName: string;
  plotName: string;
}
