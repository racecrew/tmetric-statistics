import { Component, OnInit } from '@angular/core';
import { ITMetricProject } from '../../domain/ITMetricProject';
import { TMetricProjectService } from '../Services/TMetricProjectService';
import { Router } from '@angular/router'

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  public accountId = 0;
  public projectList: ITMetricProject[];
  public errorMsg: string;

  constructor(private _projectService: TMetricProjectService, private _router: Router) {
  }

  ngOnInit() {
    this._projectService.getProjects(this.accountId)
      .subscribe(data => this.projectList = data,
        error => this.errorMsg = error
      );
  }

  onSelect(project) {
    this._router.navigate(['/projects',project.projectId]);
  }

}
