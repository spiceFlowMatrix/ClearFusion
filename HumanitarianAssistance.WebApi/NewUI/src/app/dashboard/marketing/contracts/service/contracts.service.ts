import { Injectable } from '@angular/core';
import { GlobalService } from '../../../../shared/services/global-services.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContractsService {

  constructor(private globalService: GlobalService) { }

  GetContractsList(url): Observable<any> {
    return  this.globalService.getList(url);
   }

   GetMasterPagesList(url): Observable<any> {
    return  this.globalService.getList(url);
   }

   DeleteUnitRate(url, data): Observable<any> {
    return  this.globalService.post(url, data);
   }

   GetContractById(url, data): Observable<any> {
     return this.globalService.post(url, data);
   }

   GetClientList(url): Observable<any> {
    return this.globalService.getList(url);
  }

  GetUnitRateDetailsByActivityId(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  SaveContract(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  DeleteContract(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  ApproveContract(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  GetFilteredList(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }

  PaginatedList(url, data): Observable<any> {
    return this.globalService.post(url, data);
  }


}
