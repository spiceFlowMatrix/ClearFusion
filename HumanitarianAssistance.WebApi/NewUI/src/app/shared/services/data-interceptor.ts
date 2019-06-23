import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable()
export class DataInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    // GET
    if (req.method === 'GET') {
      req = req.clone({
        headers: req.headers
          .set('Accept', 'application/json')
          .append('Access-Control-Allow-Credentials', 'true')
          .append(
            'Authorization',
            'Bearer ' + localStorage.getItem('authenticationtoken')
          )
      });
      return next.handle(req);
    }

    // POST
    if (req.method === 'POST') {
        req = req.clone({
          headers: req.headers
            .append(
              'Authorization',
              'Bearer ' + localStorage.getItem('authenticationtoken')
            )
        });
      return next.handle(req);
    }

    // PUT
    if (req.method === 'PUT') {
      req = req.clone({
        headers: req.headers
          // .set('Accept', 'application/json')
                    .append('Content-Type', 'text/plain')
      });
      return next.handle(req);
    }

    // DELETE
    if (req.method === 'DELETE') {
      return next.handle(req);
    }

    // Default
    req = req.clone({
      headers: req.headers
        .set('Accept', 'application/json')
        .append('Access-Control-Allow-Credentials', 'true')
    });
    return next.handle(req);
  }
}
