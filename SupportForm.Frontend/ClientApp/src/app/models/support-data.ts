import { RequestType } from './request-type';

export interface SupportData {
  email: string;
  phone: string;
  userId: string;
  requestType: RequestType;
  message: string;
}
