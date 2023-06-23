import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from './user-register.component';
import { UserRegisterRoutingModule } from './user-register.routing.module';
import { DxButtonModule, DxCheckBoxModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    UserRegisterComponent
  ],
  imports: [
    CommonModule,
    UserRegisterRoutingModule,
    DashboardModule,
    DxSelectBoxModule,
    DxTextBoxModule,
    DxDateBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxRadioGroupModule,
    DxButtonModule,
    ReactiveFormsModule
  ],
  exports: [UserRegisterComponent]
})
export class UserRegisterModule { }
