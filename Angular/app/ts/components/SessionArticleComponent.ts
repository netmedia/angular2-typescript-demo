import {
  Component,
  Injectable,
  bind,
  OnInit,
  ElementRef,
  EventEmitter,
  Inject
} from '@angular/core';

import { Article } from '../Models';
import { SessionService } from '../services/SessionService';

@Component({
  selector: 'session-article',
  inputs: ['article'],
  outputs: ['voteUp', 'voteDown'],
  host: {
    class: 'row'
  },
  template: `
    <div class="four wide column center aligned votes">
      <div class="ui statistic">
        <div class="value">
          {{ article.Votes }}
        </div>
        <div class="label">
          Points
        </div>
      </div>
    </div>
    <div class="twelve wide column">
      <a class="ui large header" href="{{ article.Link }}">
        {{ article.Title }}
      </a>

      <div class="meta">({{ article.domain() }})</div>
      <ul class="ui big horizontal list voters">
        <li class="item">
          <a href (click)="handleVoteUp(article.Id)">
            <i class="arrow up icon"></i>
              vote up 
            </a>
        </li>
        <li class="item"> 
          <a href (click)="handleVoteDown(article.Id)">
            <i class="arrow down icon"></i>
            vote down
          </a>
        </li>
      </ul>
    </div>
  `
})
export class SessionArticleComponent {
  article: Article;

  // voteUp:   EventEmitter<number> = new EventEmitter<number>();
  // voteDown: EventEmitter<number> = new EventEmitter<number>();

  constructor(public SessionService: SessionService) {
  }

  public handleVoteUp(id: number): boolean {
    this.SessionService.upVote(id)
    .subscribe(
      (response: any) => this.article.voteUp(),
      (err: any)      => console.log(err)
    );

    return false;
  }

  public handleVoteDown(id: number): boolean {
    if (this.article.Votes == 0) return false; 

    this.SessionService.downVote(id)
    .subscribe(
      (response: any) => this.article.voteDown(),
      (err: any)      => console.log(err)
    );

    return false;
  }
}