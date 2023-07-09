import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRoutingModule } from './i-forgot-the-password.routing.module';
import { IForgotThePasswordComponent } from './i-forgot-the-password.component';
import { DxButtonModule } from 'devextreme-angular';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    IForgotThePasswordComponent
  ],
  imports: [
    CommonModule,
    DxButtonModule,
    LoginRoutingModule,
    ReactiveFormsModule
  ],
  exports: [IForgotThePasswordComponent]
})
export class IForgotThePasswordModule { }
