import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxChartModule, DxDrawerModule, DxListModule, DxTabPanelModule, DxTemplateModule, DxTreeViewModule } from 'devextreme-angular';
import { PerfilComponent } from './perfil.component';
import { PerfilRoutingModule } from './perfil.routing.module';
import { DashboardModule } from '../../dashboard/dashboard.module';



@NgModule({
  declarations: [
    PerfilComponent
  ],
  imports: [
    CommonModule,
    DxTabPanelModule,
    DxTreeViewModule,
    DxTemplateModule,
    DxDrawerModule,
    DxListModule,
    DxChartModule,

    PerfilRoutingModule,
    DashboardModule
  ],
  bootstrap: [PerfilComponent],
  exports:[PerfilComponent]
})
export class PerfilModule { }
