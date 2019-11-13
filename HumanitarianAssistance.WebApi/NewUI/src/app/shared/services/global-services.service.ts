import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/internal/operators/map';
import { GlobalErrorHandler } from './global-error-handler';
import { CommonLoaderService } from '../common-loader/common-loader.service';
import { finalize } from 'rxjs/internal/operators/finalize';
import { Observable } from 'rxjs/internal/Observable';
import { timeout, retryWhen, tap, delay, take } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  errHandler: GlobalErrorHandler;

  constructor(private http: HttpClient, private loader: CommonLoaderService,public toastr: ToastrService) { }

  //#region  "POST"
  post(url: string, data, options?: any): Observable<any> {
    return this.http.post<any>(url, data, options).pipe(
      map((response) => response),
      timeout(200000),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      }
      )

      // catchError(this.errHandler.handleError('dfdf', []))
      // catchError((error) => { // console.log(error); return ""; })
    );
  }
  //#endregion

  //#region "GET_LIST"
  getList(url: string): Observable<any> {
    return this.http.get<any>(url).pipe(
      map((response) => response),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      })
    );
  }
  //#endregion

  //#region "GET_LIST_BY_ID"
  getListById(url: string, id: number): Observable<any> {
    return this.http.post<any>(url, id).pipe(
      map((response) => response),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      })
    );
  }

  //#region "GET_ITEM_BY_ID"
  getItemById(url: string, Id: number): Observable<any> {
    return this.http.get<any>(url + '?Id=' + Id).pipe(
      map((response) => response),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      })
    );
  }

  //#region "GET_LIST_BY_ID"
  getDataById(url: string, options?: any): Observable<any> {
    return this.http.get<any>(url, options).pipe(
      map((response) => response),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      })
    );
  }

  //#region "deleteById"
  deleteById(url: string, options?: any): Observable<any> {
    return this.http.delete<any>(url, options).pipe(
      map((response) => response),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      })
    );
  }

  //#region "GET_LIST_BY_ID And Date"
  getListByIdAndDate(url: string, data): Observable<any> {
    return this.http.post(url, data).pipe(
      map((response) => response),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      })
    );
  }

  getListByListId(url: string, id: any): Observable<any> {
    // this.loader.showLoader();
    return this.http.post<any>(url, id).pipe(
      map((response) => response),
      retryWhen(errors =>

        errors.pipe(
          tap(r => {
            this.toastr.warning('Slow internet connection retrying ...')
          }
          ),
          delay(10000),
          take(3)
        )
      ),
      finalize(() => {
        // this.loader.hideLoader();
      })
    );
  }

  //#region  "POST"
  put(url: string, data, options?: any): Observable<any> {
    return this.http.put<any>(url, data, options).pipe(
      map((response) => response),
      finalize(() => {
        // this.loader.hideLoader();
      })

      // catchError(this.errHandler.handleError('dfdf', []))
      // catchError((error) => { // console.log(error); return ""; })
    );
  }
  //#endregion

}
