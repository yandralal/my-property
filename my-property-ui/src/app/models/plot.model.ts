export interface Plot {
  id: number;
  saleId: number;
  plotNumber: string;
  propertyId: number;
  status: string;
  area: number;
  saleDate?: string;
  saleAmount: number;
  customerName: string;
  customerPhone: string;
  customerEmail: string;
  hasSale: boolean;
  amountPaid: number;
  amountBalance: number;
  selected?: boolean;
  addedAt?: string;
  agentId?: number;
  brokerageAmount?: number;
}