import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxDrawerModule, DxListModule, DxTabPanelModule, DxTemplateModule, DxTreeViewModule } from 'devextreme-angular';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard.routing.module';
import { RouterModule } from '@angular/router';



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
    DashboardRoutingModule,
    RouterModule
  ],
  bootstrap: [DashboardComponent],
  exports:[DashboardComponent]
})
export class DashboardModule { }
