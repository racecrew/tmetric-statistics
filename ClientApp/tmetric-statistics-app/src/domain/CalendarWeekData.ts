export class CalendarWeekData {
  public calendarWeek: number;
  public startOfCalendarWeek: string;
  public endOfCalendarWeek: string;
  public plannedHours: number;
  public actualHours: number;
  public overtime: number;
  public sickLeave: number;
  public holidays: number;
  public bridgeDay: number;
  public publicHoliday: number;

  constructor(
    calendarWeek?: number,
    startOfCalendarWeek?: string,
    endOfCalendarWeek?: string,
    plannedHours?: number,
    actualHours?: number,
    overtime?: number,
    sickLeave?: number,
    holidays?: number,
    bridgeDay?: number,
    publicHoliday?: number

  ) {
    this.calendarWeek = calendarWeek;
    this.startOfCalendarWeek = startOfCalendarWeek;
    this.endOfCalendarWeek = endOfCalendarWeek;
    this.plannedHours = plannedHours;
    this.actualHours = actualHours;
    this.overtime = overtime;
    this.sickLeave = sickLeave;
    this.holidays = holidays;
    this.bridgeDay = bridgeDay;
    this.publicHoliday = publicHoliday;
  }

}
