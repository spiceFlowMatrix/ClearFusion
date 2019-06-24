import { Component, OnInit, Input } from '@angular/core';
import { CodeService } from '../../../code/code.service';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';

declare let jsPDF;
declare var $;

@Component({
  selector: 'app-details-of-note',
  templateUrl: './details-of-note.component.html',
  styleUrls: ['./details-of-note.component.css']
})
export class DetailsOfNoteComponent implements OnInit {
  @Input() financialYearArr: any[];
  @Input() currencyModel: any[];
  @Input() accountTypeDropdown: any[];

  DetailOfNotesDataSource: any[];
  detailOfNotesReportDataSource: any[];
  detailsOfNoteFilterForm: DetailsOfNoteFilterModel;

  selectedCurrency: any;
  selectedFinancialYearName: any;

  // loader
  detailsOfNoteLoader = false;

  constructor(
    private codeService: CodeService,
    private setting: AppSettingsService,
  ) {}

  ngOnInit() {
    if (this.financialYearArr != null && this.currencyModel != null) {
      this.initializeForm();
    }
  }

  initializeForm() {
    this.detailsOfNoteFilterForm = {
      Currency:
        this.currencyModel != null ? this.currencyModel[0].CurrencyId : null,
      FinancialYear:
        this.financialYearArr != null
          ? this.financialYearArr[0].FinancialYearId
          : null
    };
  }

  // #region "getDetailsOfNotesReportData"
  getDetailsOfNotesReportData(FinancialYearID, CurrencyId) {
    this.showDetailsOfNoteLoader();
    this.codeService
      .GetAllDetailsOfNotes(
        this.setting.getBaseUrl() +
          GLOBAL.API_Account_GetDetailsOfNotesReportData,
        FinancialYearID,
        CurrencyId
      )
      .subscribe(
        data => {
          this.detailOfNotesReportDataSource = [];
          if (
            data.data.DetailsOfNotesFinalList != null &&
            data.StatusCode === 200
          ) {
            if (data.data.DetailsOfNotesFinalList.length > 0) {
              data.data.DetailsOfNotesFinalList.forEach(element => {
                this.detailOfNotesReportDataSource.push(element);
              });
            }

            if (this.detailsOfNoteFilterForm != null) {
              this.selectedCurrency = this.currencyModel.filter(
                x => x.CurrencyId === this.detailsOfNoteFilterForm.Currency
              )[0].CurrencyCode;
              this.selectedFinancialYearName = this.financialYearArr.filter(
                x =>
                  x.FinancialYearId ===
                  this.detailsOfNoteFilterForm.FinancialYear
              )[0].FinancialYearName;
            }
          }
          this.hideDetailsOfNoteLoader();
        },
        error => {
          this.hideDetailsOfNoteLoader();
        }
      );
  }
  //#endregion

  //#region "onFieldDetailsOfNoteFilterChanged"
  selectedDetailsOfNoteFilter(data) {
    if (data != null) {
      this.detailsOfNoteFilterForm.Currency = data.Currency;
      this.detailsOfNoteFilterForm.FinancialYear = data.FinancialYear;
      this.getDetailsOfNotesReportData(data.FinancialYear, data.Currency);
    }
  }
  //#endregion

  //#region "exportDetailsOfNotesPdf"
  exportDetailsOfNotesPdf() {
    const pdf = new jsPDF('p', 'pt', 'legal'),
      pdfConf = {
        pagesplit: false,
        background: '#fff'
      };

    pdf.addHTML($('#mainDetailOfNotesPdf'), 0, 15, pdfConf, function() {
      pdf.save('Details-of-note.pdf');
    });
  }
  //#endregion

  showDetailsOfNoteLoader() {
    this.detailsOfNoteLoader = true;
  }
  hideDetailsOfNoteLoader() {
    this.detailsOfNoteLoader = false;
  }
  //#endregion
}

class DetailsOfNoteFilterModel {
  Currency: number;
  FinancialYear: number;
}
