import { Component, OnInit } from '@angular/core';
import { ITMetricAccount } from '../../domain/ITMetricAccount'
import { TMetricUserProfileService } from '../services/TMetricUserProfileService';
import { DataExchangeService } from '../dataexchange.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {

  public accountList: ITMetricAccount[];
  public accountSelected: ITMetricAccount = null;
  public errorMsg: string;

  private userProfileServiceSubscription: Subscription; 

  constructor(
    private userProfileService: TMetricUserProfileService,
    private dataExchangeService: DataExchangeService)
  { }

  ngOnInit() {
    this.userProfileServiceSubscription = this.userProfileService.getAccounts()
      .subscribe(
        (accountList: ITMetricAccount[]) => { this.accountList = accountList; },
        (error: string) => { this.errorMsg = error; }
      );
  }

  ngOnDestroy() {
    this.userProfileServiceSubscription.unsubscribe();
  }

  doOnSelectChange() {
    this.dataExchangeService.sendAccountData(this.accountSelected);
  }
}
