import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { PermissionListRoutingModule } from './permission-list.routing.module';
import { PermissionListComponent } from './permission-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UsersService } from 'src/app/services/users/users.service';
import { PermisoService } from 'src/app/services/permisos/permiso.service';
import { ModuloService } from 'src/app/services/modulo/modulo.service';

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
    HttpClientModule,
    ReactiveFormsModule
  ],
  exports: [PermissionListComponent],
  providers: [UsersService, PermisoService, ModuloService]
})
export class PermissionListModule { }
