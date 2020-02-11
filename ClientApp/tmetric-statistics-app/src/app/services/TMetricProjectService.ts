import { Injectable } from '@angular/core';
import { HttpParams, HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ITMetricProject } from '../../domain/ITMetricProject';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class TMetricProjectService {
  private _urlProjects: string = "/api/v1/projects";

  constructor(private httpClient: HttpClient) {}

  getProjects(accountId): Observable<ITMetricProject[]> {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    headers.append('accountId', accountId);

    let params = new HttpParams().set('accountId', accountId);

    return this.httpClient.get<ITMetricProject[]>(this._urlProjects, { headers: headers, params: params })
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
