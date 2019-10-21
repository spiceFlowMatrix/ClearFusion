import { Injectable } from '@angular/core';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SchedulerService {

  constructor(private globalService: GlobalService) { }
  Post(url: string, data) {
    return this.globalService.post(url, data);
  }

  Get(url): Observable<any> {
    return  this.globalService.getList(url);
   }
}
