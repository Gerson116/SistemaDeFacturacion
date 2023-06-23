import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login.routing.module';
import { DxButtonModule } from 'devextreme-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginService } from 'src/app/services/login/login.service';



@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    DxButtonModule,
    LoginRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports:[LoginComponent],
  providers:[
    LoginService
  ]
})
export class LoginModule { }
