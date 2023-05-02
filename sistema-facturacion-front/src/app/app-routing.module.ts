import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./components/login/login.module').then(m => m.LoginModule)
  },
  {
    path: 'login',
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
    loadChildren: () => import('./components/users/user-list/user-list.module').then(m => m.UserListModule)
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./components/dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {
    path: 'perfil',
    loadChildren: () => import('./components/users/perfil/perfil.module').then(m => m.PerfilModule)
  },
  {
    path: 'listado-roles',
    loadChildren: () => import('./components/role/role-list/role-list.module').then(m => m.RoleListModule)
  },
  {
    path: 'listado-permisos',
    loadChildren: () => import('./components/permission/permission-list/permission-list.module').then(m => m.PermissionListModule)
  },
  {
    path: 'listado-ventas',
    loadChildren: () => import('./components/sales/sales-list/sales-list.module').then(m => m.SalesListModule)
  },
  {
    path: 'nueva-venta',
    loadChildren: () => import('./components/sales/add-sales/add-sales.module').then(m => m.AddSalesModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
