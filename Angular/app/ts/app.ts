/*
 * Angular
 */
import {
  Component,
  EventEmitter,
  OnInit
} from '@angular/core';
import { 
  HTTP_PROVIDERS,
  Http,
  Response,
  Headers } from '@angular/http';
import { bootstrap } from '@angular/platform-browser-dynamic';
import { Observable } from 'rxjs';

/*
 * Components
 */
import { SessionSearchComponent } from 'components/SessionSearchComponent';
import { SessionFormComponent } from 'components/SessionFormComponent';
import { SessionArticleComponent } from 'components/SessionArticleComponent';
import { SessionArticleListComponent } from 'components/SessionArticleListComponent';

/*
 * Injectables
 */
import { SessionService } from 'services/SessionService';

import { Article } from 'Models';

/*
 * Webpack
 */
require('css/styles.scss');
require("css/semantic.scss");
require("css/nm_custom.scss");

@Component({
  selector: 'session-app',
  inputs: ['addItem'],
  directives: [
    SessionSearchComponent,
    SessionFormComponent,
    SessionArticleListComponent,
    SessionArticleComponent
  ],
  providers: [SessionService],
  template: `
    <div class="ui main text container content-wrapper">
      <session-form (addItem)="createNewArticle($event)"></session-form>

      <div>
        <div class="input-group input-group-lg col-md-12 session-search-wrapper">
          <session-search (searchResults)="updateArticles($event)"></session-search>
        </div>
      </div>

      <session-article-list [articles]="sortedArticles()"></session-article-list>
    </div>
  `
})
class SessionApp {
  articles: Article[] = [];

  constructor(private http: Http, private SessionService: SessionService) {
  }

  ngOnInit(): void {
    this.SessionService.get()
    .subscribe(
      (articles: Article[]) => this.articles = articles,
      (err: any)            => console.log(err)
    );
  }

  public createNewArticle(newArticle: Article): void {
    this.SessionService.add(newArticle)
    .subscribe(
      (article: Article)  => this._addArticleToList(article),
      (err: Response)     => console.log('Error', err)
    );
  }

  public updateArticles(articles: Article[]): void {
    this.articles = articles;
  }

  private _addArticleToList(newArticle: Article): void {
    this.articles.unshift(newArticle);
  }

  sortedArticles(): Article[] {
    return this.articles.sort((a: Article, b: Article) => b.Id - a.Id);
  }
}

bootstrap(SessionApp, [
  HTTP_PROVIDERS
]);
