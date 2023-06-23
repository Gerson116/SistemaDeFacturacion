import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './security/guards/auth-guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./components/login/login.module').then(m => m.LoginModule)
  },
  {
    path: 'olvide-mi-pass',
    loadChildren: () => import('./components/i-forgot-the-password/i-forgot-the-password.module').then(m => m.IForgotThePasswordModule)
  },
  {
    path: 'registrar-usuario',
    loadChildren: () => import('./components/users/user-register/user-register.module').then(m => m.UserRegisterModule)
  },
  {
    path: 'listado-usuario',
    loadChildren: () => import('./components/users/user-list/user-list.module').then(m => m.UserListModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./components/dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'perfil',
    loadChildren: () => import('./components/users/perfil/perfil.module').then(m => m.PerfilModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'permisos',
    loadChildren: () => import('./components/permission/permission-list/permission-list.module').then(m => m.PermissionListModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'listado-ventas',
    loadChildren: () => import('./components/sales/sales-list/sales-list.module').then(m => m.SalesListModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'nueva-venta',
    loadChildren: () => import('./components/sales/add-sales/add-sales.module').then(m => m.AddSalesModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'mantenimiento-modulo',
    loadChildren: () => import('./components/module-maintenance/module-maintenance.module').then(m => m.ModuleMaintenanceModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
