import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
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
    loadChildren: () => import('./components/user-register/user-register.module').then(m => m.UserRegisterModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
