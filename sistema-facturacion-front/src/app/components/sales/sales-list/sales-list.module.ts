import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { SalesListComponent } from './sales-list.component';
import { SalesListRoutingModule } from './sales-list.routing.module';
import { SalesService } from 'src/app/services/sales/sales.service';
import { PdfFacturaComponent } from '../../pdf-factura/pdf-factura.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PdfFacturaModule } from '../../pdf-factura/pdf-factura.module';

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
    HttpClientModule,
    ReactiveFormsModule,
    PdfFacturaModule
  ],
  providers:[SalesService],
  exports: [SalesListComponent]
})
export class SalesListModule { }
