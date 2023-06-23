import { HttpClient, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import CustomStore from 'devextreme/data/custom_store';
import { Observable, lastValueFrom } from 'rxjs';
import { FiltroUsuario } from 'src/app/models/dtos/filtro-usuario';
import { ParametrosDeBusqueda } from 'src/app/models/dtos/parametros-de-busqueda';
import { UserDocument } from 'src/app/models/enums/user-document';
import { UserSelect } from 'src/app/models/enums/user-select';
import { UserState } from 'src/app/models/enums/user-state';
import { TblUsuarios } from 'src/app/models/tbl-usuarios';
import { SweetalertService } from 'src/app/services/sweetalert2/sweetalert.service';
import { UsersService } from 'src/app/services/users/users.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {

  dataSource: any;
  multipleChoices: any = [
    { viewValue: "Seleccione", value: 0 },
    { viewValue: "Nombre", value: 1 },
    { viewValue: "Identificación (Cédula)", value: 2 },
    { viewValue: "Pasaporte", value: 3 }
  ];

  selectDocuments: any = [
    { viewValue: "Seccionar documento", value: 0 },
    { viewValue: "Identificacion (Cédula)", value: 1 },
    { viewValue: "Pasaporte", value: 2 }
  ];

  userRols: any = [
    { viewValue: "Seccione el Rol", value: 0 },
    { viewValue: "Admin", value: 1 },
    { viewValue: "Usuario", value: 2 }
  ];

  formGroupFilter: FormGroup | any;
  formGroupUserRegister: FormGroup | any;

  maintenanceTitle: string;
  maintenanceState: boolean;
  tblUsuarios: TblUsuarios;

  constructor(private usersService: UsersService,
    private formBuilder: FormBuilder,
    private sweetalertService: SweetalertService){
      this.maintenanceTitle = '';
      this.tblUsuarios = new TblUsuarios();
      this.maintenanceState = false;
  }

  ngOnInit(){
    this.searchUser();
    this.buildForm();
  }

  buildForm(){
    //...
    this.formGroupFilter = this.formBuilder.group({
      selectFilter: [0, Validators.nullValidator],
      txtSearch: ['', Validators.nullValidator]
    });

    this.formGroupUserRegister = this.formBuilder.group({
      id: [0, Validators.nullValidator],
      apellido: ['', Validators.required],
      nombre: ['', Validators.required],
      nombreDeUsuario: ['', Validators.required],
      fechaDeNacimiento: [Date, Validators.required],
      selectDocuments: [0, Validators.nullValidator],
      identificacion: ['', Validators.nullValidator],
      pasaporte: ['', Validators.nullValidator],
      email: ['', Validators.compose([
        Validators.required, Validators.email
      ])],
      rolUsuario: [0, Validators.required],
      password: ['', Validators.compose([
        Validators.required, Validators.minLength(8)
      ])],
      estado: [true, Validators.required]
    });
  }

  searchUser(pagina: number = 1, cantidadDePagina: number = 10){
    this.usersService.userList().subscribe(resp => {
      this.dataSource = resp.data;
    });
  }

  filterUser(){
    //...
    // this.usersService.filterUser()
    let select: number = this.formGroupFilter.controls['selectFilter'].value;
    let parametro: FiltroUsuario = new FiltroUsuario();

    if(UserSelect.Nombre == select){
      parametro.nombre = this.formGroupFilter.controls['txtSearch'].value;
      this.usersService.filterUser(parametro).subscribe(resp => {
        this.dataSource = resp.data;
      });
    }
    if(UserSelect.Identificacion == select){
      //...
      parametro.Identificacion = this.formGroupFilter.controls['txtSearch'].value;
      this.usersService.filterUser(parametro).subscribe(resp => {
        this.dataSource = resp.data;
      });
    }
    if(UserSelect.Pasaporte == select){
      //...
      parametro.Pasaporte = this.formGroupFilter.controls['txtSearch'].value;
      this.usersService.filterUser(parametro).subscribe(resp => {
        this.dataSource = resp.data;
      });
    }
  }

  addUser(){
    this.maintenanceTitle = 'Agregar Usuario';
    this.maintenanceState = false;
  }

  editUser(element: TblUsuarios){
    this.maintenanceTitle = 'Editar Usuario';
    this.setFormData(element);
    this.maintenanceState = true;
  }

  mappingData(): TblUsuarios{
    // this.formGroupUserRegister.controls[''].value;
    let tempObj = new TblUsuarios();
    tempObj.id = this.formGroupUserRegister.controls['id'].value;
    tempObj.apellidos = this.formGroupUserRegister.controls['apellido'].value;
    tempObj.nombres = this.formGroupUserRegister.controls['nombre'].value;
    tempObj.nombreDeUsuario = this.formGroupUserRegister.controls['nombreDeUsuario'].value;
    tempObj.fechaDeNacimiento = this.formGroupUserRegister.controls['fechaDeNacimiento'].value;

    //... Esta validación se utiliza para identificar
    if(this.formGroupUserRegister.controls['selectDocuments'].value == UserDocument.NotSelected){
      tempObj.tarjetaDeIdentificacion = this.formGroupUserRegister.controls['identificacion'].value;
      tempObj.pasaporte = this.formGroupUserRegister.controls['pasaporte'].value;
    }
    if(this.formGroupUserRegister.controls['selectDocuments'].value == UserDocument.Id){
      tempObj.tarjetaDeIdentificacion = this.formGroupUserRegister.controls['identificacion'].value;
    }
    if(this.formGroupUserRegister.controls['selectDocuments'].value == UserDocument.Passport){
      tempObj.pasaporte = this.formGroupUserRegister.controls['pasaporte'].value;
    }

    tempObj.email = this.formGroupUserRegister.controls['email'].value;
    tempObj.rolId = this.formGroupUserRegister.controls['rolUsuario'].value;
    tempObj.password = this.formGroupUserRegister.controls['password'].value;
    tempObj.estadoId = (this.formGroupUserRegister.controls['estado'].value == true) ? UserState.Activo : UserState.Inactivo;
    return tempObj;
  }

  setFormData(element: TblUsuarios){
    // this.formGroupUserRegister.controls[''].setValue();
    this.formGroupUserRegister.controls['id'].setValue(element.id);
    this.formGroupUserRegister.controls['apellido'].setValue(element.apellidos);
    this.formGroupUserRegister.controls['nombre'].setValue(element.nombres);
    this.formGroupUserRegister.controls['nombreDeUsuario'].setValue(element.nombreDeUsuario);
    this.formGroupUserRegister.controls['fechaDeNacimiento'].setValue(element.fechaDeNacimiento);
    this.formGroupUserRegister.controls['identificacion'].setValue(element.tarjetaDeIdentificacion);
    this.formGroupUserRegister.controls['pasaporte'].setValue(element.pasaporte);
    this.formGroupUserRegister.controls['email'].setValue(element.email);
    this.formGroupUserRegister.controls['rolUsuario'].setValue(element.rolId);
    // this.formGroupUserRegister.controls['password'].setValue(element.password);
    let state = (this.formGroupUserRegister.controls['estado'].value == true) ? UserState.Activo : UserState.Inactivo;
    this.formGroupUserRegister.controls['estado'].setValue(state);
  }

  btnSaveUser(){
    //...
    if(this.formGroupUserRegister.valid){
      this.tblUsuarios = this.mappingData();
      if(this.maintenanceState == false){
        this.usersService.saveUser(this.tblUsuarios).subscribe(resp => {
          if(resp.succcess){
            this.sweetalertService.successMessage(resp.message);
            this.searchUser();
          }else{
            this.sweetalertService.dangerMessage(resp.data);
          }
        });
      }
      else{
        this.usersService.editUser(this.tblUsuarios).subscribe(resp => {
          this.sweetalertService.successMessage(resp.message);
          this.searchUser();
        });
      }
    }else{
      this.sweetalertService.dangerMessage("Necesita completar los campos requeridos.");
    }
  }

  btnDeleteUser(user: TblUsuarios){
    this.usersService.changeUserState(user.id, UserState.Inactivo).subscribe(resp => {
      this.sweetalertService.successMessage(resp.message);
      this.searchUser();
    });
  }
}
