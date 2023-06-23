import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuOptions } from 'src/app/models/dtos/menu-options';
import { TblModuloDTO } from 'src/app/models/dtos/modulo-dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

  showOrHiddeMenu: boolean;
  enabledModules: Array<TblModuloDTO>;
  menuOptions: Array<MenuOptions>;

  constructor(private route: Router){
    this.showOrHiddeMenu = false;
    this.menuOptions = new Array<MenuOptions>();
    this.enabledModules = Array<TblModuloDTO>();
  }

  ngOnInit(){
    this.searchOptionMenu();
  }

  searchOptionMenu(): void{
    //.... En este método se buscan las opciones del menú al que pueden acceder los usuarios.
    this.menuOptions = JSON.parse(localStorage.getItem(environment.modulos) || '{}');
  }

  btnSignOut(){
    //... This method closes the session and clears the localstorage
    localStorage.clear();
    this.route.navigateByUrl('');
  }
}
