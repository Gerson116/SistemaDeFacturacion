import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { parseDate } from 'devextreme/localization';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(): boolean {
    let fechaActual: Date = new Date();
    let temp: any = localStorage.getItem(environment.fechaExpiracion);
    let fechaExpiracion: Date = new Date(temp);
    if(fechaExpiracion > fechaActual){
      return true;
    }
    return false;
  }
}
