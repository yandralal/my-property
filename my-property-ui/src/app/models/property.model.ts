export interface Property {
  id: number;
  title: string;
  type: string;
  status: string;
  buyPrice: number;
  owner: string;
  phone: string;
  amountPaid: number;
  totalLoanPrinciple: number;
  amountBalance: number;
  khasraNo: string;
  area: number;
  address: string;
  city: string;
  state: string;
  zipCode: string;
}

export interface RegisterPropertyRequest {
  title: string;
  type: string;
  status: string;
  price: number;
  owner: string;
  phone: string;
  address: string;
  city: string;
  state: string;
  zipCode: string;
  description: string;
  khasraNo: string;
  area: number;
}