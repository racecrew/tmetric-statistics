import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { CalendarWeekData } from '../../domain/CalendarWeekData';
import { catchError } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable()
export class TMetricTimeDataService {
  private _urlCalendarWeekData: string = "/api/v1/calendarweekdata";

  constructor(private httpClient: HttpClient) { }

  getCalendarWeekData(accountId: number, userProfileId: number, startOfCalendarWeek: string, endOfCalendarWeek: string): Observable<CalendarWeekData> {

    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    let params = new HttpParams()
      .set('accountId', accountId.toString())
      .set('userProfileId', userProfileId.toString())
      .set('startOfCalendarWeek', startOfCalendarWeek)
      .set('endOfCalendarWeek', endOfCalendarWeek);

    return this.httpClient.get<CalendarWeekData>(this._urlCalendarWeekData, { headers: headers, params: params })
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
