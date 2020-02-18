import { Component, OnInit } from '@angular/core';
import { CalendarWeekData } from '../../domain/CalendarWeekData';
import { DateTimeService } from '../Services/DateTimeService';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-calendar-week',
  templateUrl: './calendar-week.component.html',
  styleUrls: ['./calendar-week.component.css']
})
export class CalendarWeekComponent implements OnInit {

  public calendarWeekData: CalendarWeekData[] = [];

  constructor(private dateTimeService: DateTimeService) {
  }

  private doInit() {
    let today = this.dateTimeService.getDateNow();
    let currentCW = this.dateTimeService.getCurrentCalendarWeek();
    for (var i = currentCW; i > 0; i--) {
      let calendarWeekDate = this.dateTimeService.timeDelta(today, (currentCW - i) * 7, "diff");
      let cwDates = this.dateTimeService.getStartAndEndOfWeek(calendarWeekDate);
      let startOfCalendarWeek = cwDates[0];
      let endOfCalendarWeek = cwDates[1];
      let calendarWeekData = new CalendarWeekData(
        i,
        formatDate(startOfCalendarWeek, "yyyy-MM-dd", "en"),
        formatDate(endOfCalendarWeek, "yyyy-MM-dd", "en")
      );
      this.calendarWeekData.push(calendarWeekData);
    }
  }

  ngOnInit() {
    this.doInit();
  }

  ngOnDestroy() {
  }

}
