import { Component, OnInit } from '@angular/core';
import { ITMetricProject } from '../../domain/ITMetricProject';
import { TMetricProjectService } from '../Services/TMetricProjectService';
import { Router } from '@angular/router'
import { DataExchangeService } from '../dataexchange.service';
import { ITMetricAccount } from '../../domain/ITMetricAccount';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  public projectList: ITMetricProject[];
  public projectSelected: ITMetricProject = null;
  public errorMsg: string;

  private accountSelected: ITMetricAccount = null;
  private accountDataServiceSubscription: Subscription;
  private projectListSubscription: Subscription;

  constructor(
    private projectService: TMetricProjectService,
    private router: Router,
    private dataExchangeService: DataExchangeService)
  {  }

  ngOnInit() {
    this.accountDataServiceSubscription = this.dataExchangeService.getAccountData()
      .subscribe((message: ITMetricAccount) => {
        if (message) {
          this.accountSelected = message;
        } else {
          this.accountSelected = null;
        }

        if (this.accountSelected) {
          this.projectListSubscription = this.projectService.getProjects(this.accountSelected.accountId)
            .subscribe(
              (projects: ITMetricProject[]) => { this.projectList = projects },
              (error: any) => { this.errorMsg = error }
            );
        }      
    });
  }

  ngOnDestroy() {
    this.projectListSubscription.unsubscribe();
    this.accountDataServiceSubscription.unsubscribe();
  }

  doOnSelectChange() {
    this.dataExchangeService.sendProjectData(this.projectSelected);
  }
}
