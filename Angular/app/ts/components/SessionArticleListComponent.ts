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
import { SessionArticleComponent } from './SessionArticleComponent';

@Component({
  selector: 'session-article-list',
  inputs: ['articles'],
  directives: [SessionArticleComponent],
  template: `
    <div class="ui grid posts">
      <session-article *ngFor="let articleInput of articles" [article]="articleInput"></session-article>
    </div>
  `
})
export class SessionArticleListComponent {
  articles: Article[];
}