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

@Component({
  selector: 'session-form',
  outputs: ['addItem'],
  template: `
    <form class="ui large form segment">
      <div class="field">
        <input name="title" #newtitle placeholder="-- title --">
      </div>
      <div class="field">
        <input name="link" #newlink placeholder="-- link --">
      </div>

      <button class="ui positive right floated button" (click)="handleAddItem(newtitle, newlink)">
        Add new session
      </button>
    </form>
  `
})
export class SessionFormComponent {
  addItem: EventEmitter<Article[]> = new EventEmitter<Article[]>();

  handleAddItem(title: HTMLInputElement, link: HTMLInputElement) : void {
    let article = new Article(title.value, link.value);

    this.addItem.next(article);

    title.value = '';
    link.value  = '';
  }
}