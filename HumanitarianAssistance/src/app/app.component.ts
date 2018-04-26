import { Component, OnInit  } from '@angular/core';
import { AppService } from './app.service';
import { AccountNoteDetail } from './Models/AccountNoteDetail';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit
{
  title = 'app';
  greetings = "";
  prodhandel=null;
  products: any;
  constructor(private _appSerivce: AppService) {

  }
 
  ngOnInit(): void {
    this._appSerivce.getProducts()
    .subscribe(result=>{
      this.products=JSON.parse(result._body);
      console.log(JSON.parse(result._body));
      
    
    });
    this._appSerivce.sayHello()
      .subscribe(
      result => {
        this.greetings = result;
      }
      );
  }
  AddNoteDetails()
  {
    console.log('click me');
    this._appSerivce.AddNote().subscribe(data=>{
     console.log(data);

    });
  }
  ngOnDistroy():void{
   //this.prodhandel.unsubscribe();
  }
}
