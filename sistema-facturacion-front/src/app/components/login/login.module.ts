import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login.routing.module';
import { DxButtonModule } from 'devextreme-angular';



@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    DxButtonModule,
    LoginRoutingModule
  ],
  exports:[LoginComponent]
})
export class LoginModule { }
