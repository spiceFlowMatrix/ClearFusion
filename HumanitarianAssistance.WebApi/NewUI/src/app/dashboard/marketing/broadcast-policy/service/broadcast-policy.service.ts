import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { PolicyModel } from '../model/policy-model';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class BroadcastPolicyService {

  constructor(private globalService: GlobalService) { }
  AddPolicy(url: string, data: PolicyModel) {
    return this.globalService.post(url, data);
  }
  GetPolicyList(url): Observable<any> {
    return  this.globalService.getList(url);
  }

  GetFilteredPolicyList(url: string, data) {
    return this.globalService.post(url, data);
  }

  DeletePolicy(url: string, data) {
    return this.globalService.post(url, data);
  }

  PolicyPaginatedList(url: string, data) {
    return this.globalService.post(url, data);
  }

  GetPolicyById(url: string, data) {
    return this.globalService.post(url, data);
  }

  Post(url: string, data) {
    return this.globalService.post(url, data);
  }

  Get(url): Observable<any> {
    return  this.globalService.getList(url);
  }

}
