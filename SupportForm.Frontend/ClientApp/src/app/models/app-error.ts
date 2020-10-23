import { ErrorType } from './error-type';

export interface AppError {
  type: ErrorType;
  message: string;
}
