import { Component, OnInit } from '@angular/core';
import { ITMetricAccount } from '../../domain/ITMetricAccount'
import { TMetricUserProfileService } from '../services/TMetricUserProfileService';
import { AccountDataService } from '../accountdata.service';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {

  public accountList: ITMetricAccount[];
  public accountIdSelected: number;
  public accountNameSelected: string;
  public errorMsg: string;

  constructor(private userProfileService: TMetricUserProfileService, private accountDataService: AccountDataService) { }

  ngOnInit() {
    this.userProfileService.getAccounts()
      .subscribe(data => this.accountList = data,
        error => this.errorMsg = error
      );
  }

  private getAccountByName(accountName: string) {
    for (var account of this.accountList) {
      if (account.accountName == accountName) {
        this.accountIdSelected = account.accountId;
        return account;
      }
    }
    return null;
  }

  doOnSelectAccountName() {
    this.accountDataService.sendMessage(this.getAccountByName(this.accountNameSelected));
  }
}
