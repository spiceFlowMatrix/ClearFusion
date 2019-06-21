import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import { CommonLoaderService, LoaderState } from './common-loader.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-common-loader',
  templateUrl: './common-loader.component.html',
  styleUrls: ['./common-loader.component.scss']
})
export class CommonLoaderComponent implements OnInit, OnDestroy {

  show = false;
  private subscription: Subscription;

  constructor(
    private loaderService: CommonLoaderService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.subscription = this.loaderService.loaderState
      .subscribe((state: LoaderState) => {
        this.show = state.show;
        this.show ? this.spinner.show() : this.spinner.hide();

      });
  }

  ngOnDestroy() {
    if (this.subscription && !this.subscription.closed) {
      this.subscription.unsubscribe();
    }
  }

}
