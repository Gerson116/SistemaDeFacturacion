import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { UserListRoutingModule } from './user-list.routing.module';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { UserListComponent } from './user-list.component';

@NgModule({
  declarations: [
    UserListComponent
  ],
  imports: [
    CommonModule,
    UserListRoutingModule,
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
  exports: [UserListComponent]
})
export class UserListModule { }
