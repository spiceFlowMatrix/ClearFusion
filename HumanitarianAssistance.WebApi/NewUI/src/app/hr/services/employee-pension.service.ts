import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class EmployeePensionService {

  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService,
    private http: HttpClient,
    private dialog: MatDialog
  ) { }
}
