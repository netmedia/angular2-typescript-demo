import {
  Component,
  Injectable,
  bind,
  OnInit,
  ElementRef,
  EventEmitter,
  Inject
} from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs';

import { Article } from '../Models';

const apiEndpoint = 'http://localhost:54461/api/Sessions/';

@Injectable()
export class SessionService {

  constructor(public http: Http) {
  }

  public search(query: string): Observable<Article[]> {
    let params: string = [
      `search=${query}`,
    ].join('&');

    let queryUrl: string = apiEndpoint + `GetAll?${params}`;

    return this.http.get(queryUrl)
      .map((response: Response) => {
        return (<any>response.json()).map(item => {
          return new Article(item.Title, item.Link, item.Votes, item.Id);
        });
      });
  }

  public get(): Observable<Article[]> {
    let url = apiEndpoint + 'GetAll';

    return this.http.get(url)
      .map((response: Response) => {
        return (<any>response.json()).map(item => {
          return new Article(item.Title, item.Link, item.Votes, item.Id);
        });
      });
  }

  public add(newArticle: Article): Observable<Article> {
    let url     = apiEndpoint + 'Create',
        data    = JSON.stringify(newArticle),
        headers = this._constructHeaders();

    return this.http.post(url, data, {headers: headers})
      .map((response: Response) => {
        var item = response.json();
        return new Article(item.Title, item.Link, item.Votes, item.Id);
      });;
  }

  public upVote(id: number) {
    let url     = apiEndpoint + 'UpVote/' + id,
        headers = this._constructHeaders();

    return this.http.post(url, null, {headers: headers});
  }

  public downVote(id: number) {
    let url     = apiEndpoint + 'DownVote/' + id,
        headers = this._constructHeaders();

    return this.http.post(url, null, {headers: headers});
  }

  private _constructHeaders() {
    let headers = new Headers();

    headers.append('Content-Type', 'application/json; charset=utf-8');

    return headers;
  }
}