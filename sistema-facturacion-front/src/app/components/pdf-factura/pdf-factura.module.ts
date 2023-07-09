import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SalesService } from 'src/app/services/sales/sales.service';
import { PdfFacturaComponent } from './pdf-factura.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    PdfFacturaComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers:[SalesService],
  exports: [PdfFacturaComponent]
})
export class PdfFacturaModule { }
