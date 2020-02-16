import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ITMetricAccount } from '../domain/ITMetricAccount';

@Injectable({
  providedIn: 'root'
})
export class AccountDataService {

  private subject = new Subject<ITMetricAccount>();
 
  sendMessage(message: ITMetricAccount) {
    this.subject.next(message);
  }

  clearMessages() {
    this.subject.next();
  }

  getMessage(): Observable<ITMetricAccount> {
    return this.subject.asObservable();
  }
}
