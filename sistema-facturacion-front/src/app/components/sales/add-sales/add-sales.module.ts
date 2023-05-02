import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { AddSalesRoutingModule } from './add-sales.routing.module';
import { AddSalesComponent } from './add-sales.component';

@NgModule({
  declarations: [
    AddSalesComponent
  ],
  imports: [
    CommonModule,
    AddSalesRoutingModule,
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
  exports: [AddSalesComponent]
})
export class AddSalesModule { }
