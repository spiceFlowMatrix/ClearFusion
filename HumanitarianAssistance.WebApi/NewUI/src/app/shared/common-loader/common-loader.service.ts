import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';

@Injectable({
  providedIn: 'root'
})
export class CommonLoaderService {

  private loaderSubject = new Subject<LoaderState>();
  loaderState = this.loaderSubject.asObservable();

  constructor() { }

  showLoader() {
    this.loaderSubject.next(<LoaderState>{ show: true });
  }

  hideLoader() {
    this.loaderSubject.next(<LoaderState>{ show: false });
  }

}


export interface LoaderState {
  show: boolean;
}
