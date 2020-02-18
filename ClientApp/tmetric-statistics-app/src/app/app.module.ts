import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { DatePipe, registerLocaleData } from '@angular/common';
import localeDe from '@angular/common/locales/de';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { TMetricProjectService } from './Services/TMetricProjectService';
import { TMetricUserProfileService } from './services/TMetricUserProfileService';
import { CalendarWeekComponent } from './calendar-week/calendar-week.component';
import { DateTimeService } from './Services/DateTimeService';

registerLocaleData(localeDe);

@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    CalendarWeekComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    TMetricProjectService,
    TMetricUserProfileService,
    DatePipe,
    DateTimeService,
    { provide: LOCALE_ID, useValue: 'de' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
