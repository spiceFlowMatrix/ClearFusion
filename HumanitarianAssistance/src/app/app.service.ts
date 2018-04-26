import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { AccountNoteDetail } from './Models/AccountNoteDetail';

@Injectable()
export class AppService {
  private greetUrl = 'api/Hello';
  private productUrl ='api/Product';
  private accountNoteUrl = "api/AddAccountNote";
  private payload : AccountNoteDetail = {AccountCode:136, BalanceType:"cvcxvv",Narration:"asd", AccountNote:900 };
  private data =  JSON.stringify(this.payload);
  private headers  =new Headers({ 'Content-Type': 'application/json' });

  // Resolve HTTP using the constructor
  constructor(private _http: Http) { }

  sayHello(): Observable<any> {
    return this._http.get(this.greetUrl).map((response: Response) => {
      return response.text();
    });
  }
  AddNote(): Observable<any>  {
    debugger;
    return this._http.post(this.accountNoteUrl,this.data, new RequestOptions({headers: this.headers })).map((res:Response)=>{
      return res;
    }).catch(this.handleError);
  }

  private handleError(error: Response){  
    console.error(error);  
    return Observable.throw(error.json().error || 'Server error');  
  } 
 getProducts() : Observable<any> {
   return this._http.get(this.productUrl).map((response:Response)=>{
     return response;
   });

 
   
   
 }
}
