import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxDrawerModule, DxListModule, DxTabPanelModule, DxTemplateModule, DxTreeViewModule } from 'devextreme-angular';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard.routing.module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    DxTabPanelModule,
    DxTreeViewModule,
    DxTemplateModule,
    DxDrawerModule,
    DxListModule,
    DashboardRoutingModule
  ],
  exports:[DashboardComponent]
})
export class DashboardModule { }
