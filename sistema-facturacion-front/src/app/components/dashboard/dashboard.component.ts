import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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

  showIconPageColor: boolean = false;
  cambioDeColorFormGroup: FormGroup;
  colorDeFondo: string = '#fff';
  colorDeLaLetra: string = '#000';

  constructor(private route: Router, private formBuilder: FormBuilder){
    this.showOrHiddeMenu = false;
    this.menuOptions = new Array<MenuOptions>();
    this.enabledModules = Array<TblModuloDTO>();
  }

  ngOnInit(){
    // this.buildForm();
    this.searchOptionMenu();
  }

  buildForm(){
    this.cambioDeColorFormGroup = this.formBuilder.group({
      selectColor: [false, Validators.nullValidator]
    });
  }

  searchOptionMenu(): void{
    //.... En este método se buscan las opciones del menú al que pueden acceder los usuarios.
    this.menuOptions = JSON.parse(localStorage.getItem(environment.modulos) || '{}');
  }

  cambiarElColorDeLaPagina(){
    if (this.cambioDeColorFormGroup.controls['selectColor'].value == false) {
      console.log('Se cambio a blanco');
      this.colorDeFondo = '#fff';
      this.colorDeLaLetra = '#000';
    }
    else{
      console.log('Se cambio a negro');
      this.colorDeFondo = '#303030';
      this.colorDeLaLetra = '#fff';
    }
  }

  btnSignOut(){
    //... This method closes the session and clears the localstorage
    localStorage.clear();
    this.route.navigateByUrl('');
  }
}
