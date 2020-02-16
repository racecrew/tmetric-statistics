import { Component, OnInit } from '@angular/core';
import { ITMetricProject } from '../../domain/ITMetricProject';
import { TMetricProjectService } from '../Services/TMetricProjectService';
import { Router } from '@angular/router'
import { AccountDataService } from '../accountdata.service';
import { ITMetricAccount } from '../../domain/ITMetricAccount';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  public accountSelected: ITMetricAccount;
  public projectList: ITMetricProject[];
  public errorMsg: string;
  private accountDataServiceSubscription: Subscription;
  private projectListSubscription: Subscription;

  constructor(
    private projectService: TMetricProjectService,
    private router: Router,
    private accountDataService: AccountDataService) {
  }

  ngOnInit() {
    this.accountDataServiceSubscription = this.accountDataService.getMessage()
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
}
