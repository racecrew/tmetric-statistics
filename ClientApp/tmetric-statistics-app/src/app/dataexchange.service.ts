import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ITMetricAccount } from '../domain/ITMetricAccount';
import { ITMetricProject } from '../domain/ITMetricProject';

@Injectable({
  providedIn: 'root'
})
export class DataExchangeService {

  private accountSubject = new Subject<ITMetricAccount>();
  private projectSubject = new Subject<ITMetricProject>();
 
  sendAccountData(accountData: ITMetricAccount) {
    this.accountSubject.next(accountData);
  }

  getAccountData(): Observable<ITMetricAccount> {
    return this.accountSubject.asObservable();
  }

  sendProjectData(projectData: ITMetricProject) {
    this.projectSubject.next(projectData);
  }

  getProjectData(): Observable<ITMetricProject> {
    return this.projectSubject.asObservable();
  }
}
