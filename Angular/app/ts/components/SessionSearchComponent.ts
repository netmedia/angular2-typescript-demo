import {
  Component,
  Injectable,
  bind,
  OnInit,
  ElementRef,
  EventEmitter,
  Inject
} from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';

import { SessionService } from '../services/SessionService';

import { Article } from '../Models';

let loadingGif: string = ((<any>window).__karma__) ? '' : require('images/loading.gif');

@Component({
  outputs: ['loading', 'searchResults'],
  selector: 'session-search',
  template: `
    <input type="text" class="form-control session-search-input" placeholder="-- session search --">
  `
})
export class SessionSearchComponent implements OnInit {
  loading: EventEmitter<boolean> = new EventEmitter<boolean>();
  searchResults: EventEmitter<Article[]> = new EventEmitter<Article[]>();

  constructor(public SessionService: SessionService, private el: ElementRef) {
  }

  ngOnInit(): void {
    // convert the `keyup` event into an observable stream
    Observable.fromEvent(this.el.nativeElement, 'keyup')
      .map((e: any) => e.target.value) // extract the value of the input
      // .filter((text: string) => text.length > 1) // filter out if empty
      .debounceTime(250)                         // only once every 250ms
      .do(() => this.loading.next(true))         // enable loading
      // search, discarding old events if new input comes in
      .map((query: string) => this.SessionService.search(query))
      .switch()
      // act on the return of the search
      .subscribe(
        (results: Article[]) => { // on sucesss
          this.loading.next(false);
          this.searchResults.next(results);
        },
        (err: any) => { // on error
          console.log(err);
          this.loading.next(false);
        },
        () => { // on completion
          this.loading.next(false);
        }
      );

  }
}