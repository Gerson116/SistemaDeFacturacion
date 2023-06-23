import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { UsersService } from 'src/app/services/users/users.service';
import { ModuleMaintenanceComponent } from './module-maintenance.component';
import { ModuleMaintenanceRoutingModule } from './module-maintenance.routing.module';
import { DashboardModule } from '../dashboard/dashboard.module';
import { ModuloService } from 'src/app/services/modulo/modulo.service';

@NgModule({
  declarations: [
    ModuleMaintenanceComponent
  ],
  imports: [
    CommonModule,
    ModuleMaintenanceRoutingModule,
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
    ReactiveFormsModule
  ],
  exports: [ModuleMaintenanceComponent],
  providers: [ModuloService]
})
export class ModuleMaintenanceModule { }
