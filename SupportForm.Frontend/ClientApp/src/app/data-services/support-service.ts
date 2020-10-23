import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from '../../environments';
import { SupportData } from '../models/support-data';
import { catchError} from 'rxjs/operators';
import { ErrorType } from '../models/error-type';
import { AppError } from '../models/app-error';

export const BASE_URL = `${environment.supportMessageApiEndpoint}`;

@Injectable()
export class SupportService {
  constructor(private http: HttpClient) { }

  postSupportMessage(supportData: SupportData): Observable<SupportData> {
    return this.http.post<SupportData>(BASE_URL, supportData)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let appError: AppError;
    if (error.error instanceof ErrorEvent) {
      appError = {
        type: ErrorType.NetworkError,
        message: 'Please, check your network connect and try again. If it fails, please contact us with support@support.com'
      };
    } else {
      appError = {
        type: ErrorType.NetworkError,
        message: 'Sorry, we are having troubles. Please, try again later. If it fails, please contact us with support@support.com'
      };
    }

    return throwError(appError);
  }
}
