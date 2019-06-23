import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreComponent } from './store.component';
import { DxTabsModule, DxDataGridModule, DxSelectBoxModule, DxCheckBoxModule, DxNumberBoxModule, DxTabPanelModule, DxButtonModule, DxFormModule, DxPopupModule, DxDateBoxModule, DxLookupModule, DxTemplateModule, DxListModule, DxTextBoxModule, DxRadioGroupModule, DxSchedulerModule, DxSwitchModule, DxTileViewModule, DxScrollViewModule, DxMenuModule, DxFileUploaderModule, DxPopoverModule, DxTagBoxModule, } from 'devextreme-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { StoreRoutingModule } from './store-routing.module';
import { StoreMasterComponent } from './store-master/store-master.component';
import { StoreMainComponent } from './store-main/store-main.component';
import { InventoryMasterComponent } from './store-master/inventory-master/inventory-master.component';
import { StoreService } from './store.service';
import { InventoryItemMasterComponent } from './store-master/inventory-item-master/inventory-item-master.component';
import { ItemTypeMasterComponent } from './store-master/item-type-master/item-type-master.component';
import { PurchasesComponent } from './store-main/purchases/purchases.component';
import { OrdersComponent } from './store-main/orders/orders.component';
import { UnitTypeMasterComponent } from './store-master/unit-type-master/unit-type-master.component';
import { PurchasesDocumentComponent } from './store-main/purchases/purchases-document/purchases-document.component';
import { ProcurmentSummaryComponent } from './procurment-summary/procurment-summary.component';
import { StoreDepreciationReportComponent } from './store-depreciation-report/store-depreciation-report.component';
import { ItemSpecificationMasterComponent } from './store-master/item-specification-master/item-specification-master.component';
import { StoreSourceCodesComponent } from './store-source-codes/store-source-codes.component';
import { PaymentTypesComponent } from './payment-types/payment-types.component';

@NgModule({
  imports: [
    StoreRoutingModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    DxDataGridModule,
    DxSelectBoxModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxTabPanelModule,
    DxButtonModule,
    DxFormModule,
    DxPopupModule,
    DxDateBoxModule,
    DxLookupModule,
    DxTabsModule,
    DxTemplateModule,
    DxListModule,
    DxTextBoxModule,
    DxRadioGroupModule,
    DxSchedulerModule,
    DxSwitchModule,
    DxTileViewModule,
    DxScrollViewModule,
    DxMenuModule,
    DxFileUploaderModule,
    DxPopoverModule,
    DxTagBoxModule,
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.5)',
      backdropBorderRadius: '0px',
      fullScreenBackdrop: true,
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    }),
  ],
  declarations: [
    StoreComponent,
    PurchasesComponent,
    OrdersComponent,
    StoreMasterComponent,
    StoreMainComponent,
    InventoryMasterComponent,
    InventoryItemMasterComponent,
    ItemTypeMasterComponent,
    UnitTypeMasterComponent,
    PurchasesDocumentComponent,
    ProcurmentSummaryComponent,
    StoreDepreciationReportComponent,
    ItemSpecificationMasterComponent,
    StoreSourceCodesComponent,
    PaymentTypesComponent
  ],
  providers:[
    StoreService
  ]
})
export class StoreModule { }
