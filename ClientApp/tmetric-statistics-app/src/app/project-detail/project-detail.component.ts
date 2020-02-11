import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {

  public projectId;

  constructor(private _route: ActivatedRoute) { }

  ngOnInit() {
    let id = parseInt(this._route.snapshot.paramMap.get('id'));
    this.projectId = id;
  }

}
