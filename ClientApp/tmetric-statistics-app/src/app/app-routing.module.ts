import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectListComponent } from './project-list/project-list.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AppComponent } from './app.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { HomeComponent } from './home/home.component';
import { AccountListComponent } from './account-list/account-list.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'projects', component: ProjectListComponent },
  { path: 'projects/:id', component: ProjectDetailComponent },
  { path: 'accounts', component: AccountListComponent },
  { path: '', redirectTo: "home", pathMatch: "full" },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
export const routingComponents = [
  AppComponent,
  HomeComponent,
  ProjectListComponent,
  ProjectDetailComponent,
  AccountListComponent,
  PageNotFoundComponent
]
