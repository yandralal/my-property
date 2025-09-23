export interface Agent {
  id: number;
  name: string;
  contact: string;
  agency: string;
  isDeleted: boolean;
  createdBy: string;
  createdDate?: string;
  modifiedBy: string;
  modifiedDate?: string;
}
