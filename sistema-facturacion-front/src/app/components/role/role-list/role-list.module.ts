import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { RoleListRoutingModule } from './role-list.routing.module';
import { RoleListComponent } from './role-list.component';



@NgModule({
  declarations: [
    RoleListComponent
  ],
  imports: [
    CommonModule,
    RoleListRoutingModule,
    DashboardModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxDateBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxRadioGroupModule,
    DxButtonModule
  ],
  exports: [RoleListComponent]
})
export class RoleListModule { }
