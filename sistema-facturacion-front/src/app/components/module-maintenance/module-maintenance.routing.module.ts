import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { ModuleMaintenanceComponent } from './module-maintenance.component';

const routes: Routes = [
  { path: '', component: ModuleMaintenanceComponent }
]; // sets up routes constant where you define your routes

// configures NgModule imports and exports
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ModuleMaintenanceRoutingModule { }
