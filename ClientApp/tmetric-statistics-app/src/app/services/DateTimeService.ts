import { Injectable } from '@angular/core';
import { DatePipe, formatDate } from '@angular/common';

@Injectable()
export class DateTimeService {

  constructor(private datePipe: DatePipe) { }

  public getCurrentCalendarWeek()
  {
    return parseInt(this.datePipe.transform(Date.now(), 'w'));
  }

  public getDateNow() {
    return new Date(Date.parse(Date()));
  }

  public makeDate(year, month, day: number)
  {
    return new Date(year, month, day);
    let date = new Date();
  }

  public getWeekDay(date: Date)
  {
    return date.getDay();
  }

  public timeDelta(deltaDate: Date, deltaDays: number, deltaOption: string) {
    let date = new Date();
    if (deltaOption == "add") {
      date.setDate(deltaDays + deltaDate.getDate());
    } else if (deltaOption == "diff") {
      date.setDate(deltaDate.getDate() - deltaDays);
    } else {
      date.setDate(deltaDate.getDate());
    }
    return date;
  }

  public getStartAndEndOfWeek(date)
  {
    let weekMap = [6, 0, 1, 2, 3, 4, 5];

    let now = new Date(date);
    let monday = new Date(now);
    monday.setDate(monday.getDate() - weekMap[monday.getDay()]);
    let sunday = new Date(now);
    sunday.setDate(sunday.getDate() - weekMap[sunday.getDay()] + 6);

    return [monday, sunday];
  }
}
