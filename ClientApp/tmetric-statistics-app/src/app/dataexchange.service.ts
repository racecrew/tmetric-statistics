import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ITMetricAccount } from '../domain/ITMetricAccount';
import { ITMetricProject } from '../domain/ITMetricProject';
import { CalendarWeekData } from '../domain/CalendarWeekData';

@Injectable({
  providedIn: 'root'
})
export class DataExchangeService {

  private accountSubject = new Subject<ITMetricAccount>();
  private projectSubject = new Subject<ITMetricProject>();
  private calendarWeekDataSubject = new Subject<CalendarWeekData>();
 
  sendAccountData(accountData: ITMetricAccount) {
    this.accountSubject.next(accountData);
  }

  getAccountData(): Observable<ITMetricAccount> {
    return this.accountSubject.asObservable();
  }

  clearAccountData() {
    this.accountSubject.next();
  }

  sendProjectData(projectData: ITMetricProject) {
    this.projectSubject.next(projectData);
  }

  getProjectData(): Observable<ITMetricProject> {
    return this.projectSubject.asObservable();
  }

  clearProjectData() {
    this.projectSubject.next();
  }

  sendCalendarWeekData(calendarWeekData: CalendarWeekData) {
    this.calendarWeekDataSubject.next(calendarWeekData);
  }

  getCalendarWeekData(): Observable<CalendarWeekData> {
    return this.calendarWeekDataSubject.asObservable();
  }

  clearCalendarWeekData() {
    this.calendarWeekDataSubject.next();
  }

}
