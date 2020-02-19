import { Component, OnInit } from '@angular/core';
import { CalendarWeekData } from '../../domain/CalendarWeekData';
import { DateTimeService } from '../Services/DateTimeService';
import { formatDate } from '@angular/common';
import { TMetricTimeDataService } from '../services/TMetricTimeDataService';
import { Subscription } from 'rxjs';
import { DataExchangeService } from '../dataexchange.service';
import { ITMetricAccount } from '../../domain/ITMetricAccount';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-calendar-week',
  templateUrl: './calendar-week.component.html',
  styleUrls: ['./calendar-week.component.css']
})
export class CalendarWeekComponent implements OnInit {

  public overallOvertime: number;
  public overallOvertimeWithoutCurrentWeek: number;
  public calendarWeekData: CalendarWeekData[] = [];
  public errorMsg: string;

  private accountSelected: ITMetricAccount = null;
  private userProfileId: number = 0;

  private accountDataServiceSubscription: Subscription;

  constructor(
    private dateTimeService: DateTimeService,
    private tmetricTimeDataService: TMetricTimeDataService,
    private dataExchangeService: DataExchangeService)
  { }

  private doInit() {
    this.overallOvertime = 0; // reset overtime
    this.overallOvertimeWithoutCurrentWeek = 0; // reset overtime
    this.calendarWeekData.length = 0; // clear array

    let today = this.dateTimeService.getDateNow();
    let currentCW = this.dateTimeService.getCurrentCalendarWeek();
    for (var i = currentCW; i > 0; i--) {
      let calendarWeekDate = this.dateTimeService.timeDelta(today, (currentCW - i) * 7, "diff");
      let cwDates = this.dateTimeService.getStartAndEndOfWeek(calendarWeekDate);
      let startOfCalendarWeek = formatDate(cwDates[0], "yyyy-MM-dd", "en");
      let endOfCalendarWeek = formatDate(cwDates[1], "yyyy-MM-dd", "en");
      let cwNumber = i;

      this.tmetricTimeDataService.getCalendarWeekData(this.accountSelected.accountId, this.userProfileId, startOfCalendarWeek, endOfCalendarWeek)
        .subscribe(
          (calendarWeekData: CalendarWeekData) => {
            calendarWeekData.calendarWeek = cwNumber;

            this.overallOvertime = this.overallOvertime + calendarWeekData.overtime;

            if ((currentCW - i) != 0 && (calendarWeekData.overtime != -40)) {
              this.overallOvertimeWithoutCurrentWeek = this.overallOvertimeWithoutCurrentWeek + calendarWeekData.overtime;
            }

            this.calendarWeekData.push(calendarWeekData);
            this.calendarWeekData.sort(function (a, b) { return b.calendarWeek - a.calendarWeek; });
          },
          (error: string) => { this.errorMsg = error; }
      );
    }
  }

  ngOnInit() {
    this.accountDataServiceSubscription = this.dataExchangeService.getAccountData()
      .subscribe((message: ITMetricAccount) => {
        if (message) {
          this.accountSelected = message;

          this.doInit();

        } else {
          this.accountSelected = null;
        }
      });
  }

  ngOnDestroy() {
    this.accountDataServiceSubscription.unsubscribe();
  }

}
