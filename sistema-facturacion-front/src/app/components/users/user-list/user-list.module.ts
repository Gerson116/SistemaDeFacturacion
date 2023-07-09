import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxRadioGroupModule, DxSelectBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { UserListRoutingModule } from './user-list.routing.module';
import { DashboardModule } from '../../dashboard/dashboard.module';
import { HttpClientModule } from '@angular/common/http';
import { UserListComponent } from './user-list.component';
import { UsersService } from 'src/app/services/users/users.service';
import { ReactiveFormsModule } from '@angular/forms';
import { UserMaintenanceComponent } from '../user-maintenance/user-maintenance.component';

@NgModule({
  declarations: [
    UserListComponent,
    UserMaintenanceComponent
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
    HttpClientModule,
    ReactiveFormsModule
  ],
  exports: [UserListComponent],
  providers: [UsersService]
})
export class UserListModule { }
