import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { SalesListComponent } from './sales-list.component';
import { SalesListRoutingModule } from './sales-list.routing.module';

@NgModule({
  declarations: [
    SalesListComponent
  ],
  imports: [
    CommonModule,
    SalesListRoutingModule,
    DashboardModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxDateBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxRadioGroupModule,
    DxButtonModule,
    DxDataGridModule,
    HttpClientModule
  ],
  exports: [SalesListComponent]
})
export class SalesListModule { }
