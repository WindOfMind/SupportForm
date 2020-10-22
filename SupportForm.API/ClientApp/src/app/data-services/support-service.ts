import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from '../../environments';
import { SupportData } from '../models/support-data';
import { catchError} from 'rxjs/operators';

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
    if (error.error instanceof ErrorEvent) {
      // it's possible to log client side errors here.
      return throwError(
        'Please, check your network connect and try again. If it fails, please contact us with support@support.com');
    } else {
        // it's possible to log backend errors here.
      return throwError(
          'Sorry, we are having troubles. Please, try again later. If it fails, please contact us with support@support.com');
    }
  }
}
