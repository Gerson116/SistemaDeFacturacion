import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IniciarSesion } from 'src/app/models/dtos/iniciar-sesion';
import { LoginService } from 'src/app/services/login/login.service';
import { SweetalertService } from 'src/app/services/sweetalert2/sweetalert.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{

  formGroupIniciarSesion: FormGroup | any;
  objIniciarSesion: IniciarSesion;

  constructor(private loginService: LoginService,
              private formBuilder: FormBuilder,
              private sweetAlert: SweetalertService,
              private router: Router){
                this.objIniciarSesion = new IniciarSesion();
  }

  ngOnInit(){
    this.buildForm();
  }

  buildForm(){
    this.formGroupIniciarSesion = this.formBuilder.group({
      email: ['', Validators.compose([
        Validators.email, Validators.required
      ])],
      pass: ['']
    });
  }

  iniciarSesion(){
    this.objIniciarSesion.email = this.formGroupIniciarSesion.controls['email'].value;
    this.objIniciarSesion.password = this.formGroupIniciarSesion.controls['pass'].value;
    localStorage.clear();
    this.loginService.iniciarSesion(this.objIniciarSesion).subscribe(resp => {
      if(resp.succcess){
        localStorage.setItem(environment.token, resp.token);
        localStorage.setItem(environment.usuario, JSON.stringify(resp.data.usuarios));
        localStorage.setItem(environment.rolId, resp.data.rolId);
        localStorage.setItem(environment.fechaExpiracion, resp.fechaExpiracion.toString());
        localStorage.setItem(environment.permisos, JSON.stringify(resp.data.permisos));
        localStorage.setItem(environment.modulos, JSON.stringify(resp.data.modulos));
        this.router.navigateByUrl('perfil');
      }
      else{
        this.sweetAlert.dangerMessage(`${resp.data}`);
      }
    });
  }

  olvideMiPass(){
  }

}
