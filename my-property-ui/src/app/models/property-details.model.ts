export interface PropertyDetailsResponse {
  id: number;
  title: string;
  type: string;
  status: string;
  owner: string;
  phone: string;
  address: string;
  city: string;
  state: string;
  zipCode: string;
  description: string;
  khasraNo: string;
  area: number;
  buyPrice: number;
  amountPaid: number;
  totalLoanPrinciple: number;
  amountBalance: number;
  totalPlots: number;
  availablePlots: number;
  bookedPlots: number;
  totalSaleAmount: number;
  totalPlotsPaid: number;
  totalPlotsBalance: number;
  totalBrokerage: number;
  totalLoanInterest: number;
  profitLossAfterLoan: number;
}
