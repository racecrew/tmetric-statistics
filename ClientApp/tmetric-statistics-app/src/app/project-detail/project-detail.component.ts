import { Component, OnInit } from '@angular/core';
import { DataExchangeService } from '../dataexchange.service';
import { Subscription } from 'rxjs';
import { ITMetricProject } from '../../domain/ITMetricProject';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {

  public projectSelected: ITMetricProject = null;
  public errMsg: string;

  private projectSubscription: Subscription;

  constructor(
    private dataExchangeServie: DataExchangeService)
  { }

  ngOnInit() {
    this.projectSubscription = this.dataExchangeServie.getProjectData()
      .subscribe(
        (projectSelected: ITMetricProject) => {
          this.projectSelected = projectSelected;
        },
        (error: any) => { this.errMsg = error; }
      );
  }

  ngOnDestroy() {
    this.projectSubscription.unsubscribe();
  }
}
