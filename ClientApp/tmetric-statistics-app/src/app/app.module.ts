import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AgGridModule } from 'ag-grid-angular';

import { TableModule } from 'primeng/table';

import { AppRoutingModule, routingComponents } from './app-routing.module';
import { TMetricProjectService } from './Services/TMetricProjectService';
import { TMetricUserProfileService } from './services/TMetricUserProfileService';

@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AgGridModule.withComponents([]),
    TableModule,
    AppRoutingModule
  ],
  providers: [
    TMetricProjectService,
    TMetricUserProfileService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
