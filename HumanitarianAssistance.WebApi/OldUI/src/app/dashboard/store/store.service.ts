import { Injectable } from '@angular/core';
import {
  Http,
  Headers,
  Response,
  RequestOptions,
  RequestOptionsArgs
} from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class StoreService {
  constructor(private http: Http) {}

  GetAllDetailsByURL(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllItemSpecification(url: string, officeId: number, itemTypeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?officeId=' + officeId + '&ItemTypeId=' + itemTypeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllDetailsById(url: string, idName: string, id: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?' + idName + '=' + id, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllPurchaseInvoices(url: string, PurchaseId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?' + 'PurchaseId=' + PurchaseId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetAllItemSpecificationForMainPage(
    url: string,
    itemTypeId: number,
    itemId: number,
    officeId: number
  ) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url +
          '?ItemTypeId=' +
          itemTypeId +
          '&itemId=' +
          itemId +
          '&OfficeId=' +
          officeId,
        options
      )
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  AddEditByModel(url: string, model: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .post(url, JSON.stringify(model), options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  // EditInventoryDetail(url: string, model: any) {
  //     let Myheaders = new Headers();
  //     Myheaders.append("Authorization", "Bearer " + localStorage.getItem("authenticationtoken"));
  //     Myheaders.append("Content-Type", "application/json");
  //     let options = new RequestOptions({ headers: Myheaders });
  //     return this.http.post(url, JSON.stringify(model)
  //         , options)
  //         .map((response: Response) => {
  //             let journal = response.json();
  //             if (journal) {
  //                 return journal;
  //             }
  //         }).catch(this.handleError);

  // }

  GetInventoryCode(url: string, InventoryId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?' + 'Id=' + InventoryId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetInventoryItemCode(url: string, InventoryId: any, TypeId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?' + 'Id=' + InventoryId + '&TypeId=' + TypeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetSourceCode(url: string, typeId?: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?typeId=' + typeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetSourceCodeType(url: string) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetStoreSourceCode(url: string, CodeTypeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?CodeTypeId=' + CodeTypeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  DeleteStoreSourceCode(url: string, SourceCodeId: number) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(url + '?Id=' + SourceCodeId, options)
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  GetProcurementSummary(url: string, EmployeeId: any, CurrencyId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .get(
        url + '?EmployeeId=' + EmployeeId + '&CurrencyId=' + CurrencyId,
        options
      )
      .map((response: Response) => {
        const codelist = response.json();
        if (codelist) {
          return codelist;
        }
      })
      .catch(this.handleError);
  }

  // DeletePaymentTypes(url: string, PaymentId: number) {

  //     let Myheaders = new Headers();
  //     Myheaders.append("Authorization", "Bearer " + localStorage.getItem("authenticationtoken"));
  //     let options = new RequestOptions({ headers: Myheaders });
  //     return this.http.get(url , options)
  //         .map((response: Response) => {
  //             let codelist = response.json();
  //             if (codelist) {
  //                 return codelist;
  //             }
  //         }).catch(this.handleError);
  // }

  DeletePaymentTypes(url: string, PaymentId: any) {
    const Myheaders = new Headers();
    Myheaders.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('authenticationtoken')
    );
    Myheaders.append('Content-Type', 'application/json');
    const options = new RequestOptions({ headers: Myheaders });
    return this.http
      .delete(url + '?PaymentId=' + PaymentId, options)
      .map((response: Response) => {
        const journal = response.json();
        if (journal) {
          return journal;
        }
      })
      .catch(this.handleError);
  }

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Server error');
  }
}
