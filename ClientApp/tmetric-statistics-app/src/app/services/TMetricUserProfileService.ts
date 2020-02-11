import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { ITMetricAccount } from '../../domain/ITMetricAccount';
import { catchError } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class TMetricUserProfileService  {
  private _urlAccounts: string = "/api/v1/accounts";

  constructor(private httpClient: HttpClient) {}

  getAccounts(): Observable<ITMetricAccount[]> {
    return this.httpClient.get<ITMetricAccount[]>(this._urlAccounts)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError('Something bad happened; please try again later.');
  }
}
