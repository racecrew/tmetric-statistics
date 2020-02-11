import { Component, OnInit } from '@angular/core';
import { ITMetricAccount } from '../../domain/ITMetricAccount'
import { TMetricUserProfileService } from '../services/TMetricUserProfileService';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {

  public accountList: ITMetricAccount[];
  public errorMsg: string;

  constructor(private userProfileService: TMetricUserProfileService) { }

  ngOnInit() {
    this.userProfileService.getAccounts()
      .subscribe(data => this.accountList = data,
        error => this.errorMsg = error
      );
  }
}
