import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { PermissionListRoutingModule } from './permission-list.routing.module';
import { PermissionListComponent } from './permission-list.component';

@NgModule({
  declarations: [
    PermissionListComponent
  ],
  imports: [
    CommonModule,
    PermissionListRoutingModule,
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
  exports: [PermissionListComponent]
})
export class PermissionListModule { }
