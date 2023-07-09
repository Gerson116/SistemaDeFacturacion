import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { AddSalesRoutingModule } from './add-sales.routing.module';
import { AddSalesComponent } from './add-sales.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SalesService } from 'src/app/services/sales/sales.service';
import { PdfFacturaModule } from '../../pdf-factura/pdf-factura.module';

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
    HttpClientModule,
    ReactiveFormsModule,
    PdfFacturaModule
  ],
  providers: [SalesService],
  exports: [AddSalesComponent]
})
export class AddSalesModule { }
