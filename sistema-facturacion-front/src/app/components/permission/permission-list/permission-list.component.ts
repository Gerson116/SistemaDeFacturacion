import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FiltroUsuario } from 'src/app/models/dtos/filtro-usuario';
import { TblModuloDTO } from 'src/app/models/dtos/modulo-dto';
import { ParametrosDeBusqueda } from 'src/app/models/dtos/parametros-de-busqueda';
import { PermisoDTO } from 'src/app/models/dtos/permiso-dto';
import { ElementToChanges } from 'src/app/models/enums/element-to-changes';
import { UserSelect } from 'src/app/models/enums/user-select';
import { UserState } from 'src/app/models/enums/user-state';
import { TblUsuarios } from 'src/app/models/tbl-usuarios';
import { ModuloService } from 'src/app/services/modulo/modulo.service';
import { PermisoService } from 'src/app/services/permisos/permiso.service';
import { SweetalertService } from 'src/app/services/sweetalert2/sweetalert.service';
import { UsersService } from 'src/app/services/users/users.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-permission-list',
  templateUrl: './permission-list.component.html',
  styleUrls: ['./permission-list.component.css', '../../../shared/css/dashboard-start.css']
})
export class PermissionListComponent {

  title: string;
  dataSource: Array<TblModuloDTO>;
  multipleChoices: any = [
    { viewValue: "Seleccione", value: 0 },
    { viewValue: "Nombre", value: 1 },
    { viewValue: "Identificación (Cédula)", value: 2 },
    { viewValue: "Pasaporte", value: 3 }
  ];

  formGroupFilter: FormGroup | any;
  formGroupPermission: FormGroup | any;

  state: string;
  colorState: string;


  //.....Usuario de prueba
  usuarioId: number = 1;
  userData: TblUsuarios;
  //.....Usuario de prueba

  listPermission: Array<PermisoDTO>;

  constructor(private formBuilder: FormBuilder,
    private permisoService: PermisoService,
    private usersService: UsersService,
    private moduloService: ModuloService,
    private sweetAlert: SweetalertService){
    this.title = 'Listar Permiso';
    this.state = 'Estado';
    this.colorState = 'black';
    this.listPermission = new Array<PermisoDTO>();
    this.userData = new TblUsuarios();
    this.dataSource = new Array<TblModuloDTO>();
  }

  ngOnInit(){
    this.buildForm();
    this.searchModule();
  }

  buildForm(){
    this.formGroupFilter = this.formBuilder.group({
      usuarioId: [0, Validators.required],
      rolId: [0, Validators.nullValidator],
      cedula: ['', Validators.required],
      apellido: ['', Validators.nullValidator],
      nombre: ['', Validators.nullValidator],
      nombreDeUsuario: ['', Validators.nullValidator],
      id: [0, Validators.nullValidator]
    });

    this.formGroupPermission = this.formBuilder.group({
      moduloId: [0, Validators.required],
      nombreDelModulo: ['', Validators.required],
      c: [false],
      r: [false],
      u: [false],
      d: [false]
    });
  }

  searchUser(){
    //... Este método debe consultar el localstorage para buscar el objeto que contiene el
    //... usuarioid y otros datos.

    this.formGroupPermission.controls['c'].setValue(false);
    this.formGroupPermission.controls['r'].setValue(false);
    this.formGroupPermission.controls['u'].setValue(false);
    this.formGroupPermission.controls['d'].setValue(false);
    this.listPermission = new Array<PermisoDTO>();

    let tempCedula: string = this.formGroupFilter.controls['cedula'].value;
    this.usersService.filterUserPerId(tempCedula).subscribe(resp => {
      if(resp.succcess){
        this.mappingDataUser(resp.data);
        this.userData = resp.data;

        this.permisoService.getAllPermisos(this.userData.id).subscribe(resp => {
          if(resp.succcess == true){
            let tempPermisosAsignados: Array<PermisoDTO> = new Array<PermisoDTO>();
            tempPermisosAsignados = resp.data;
            for(let i = 0; i < tempPermisosAsignados.length; i++){
              this.addElementExistToList(tempPermisosAsignados[i]);
            }
          }
        });
      }
      else{
        this.sweetAlert.dangerMessage('El usuario que busco por la cédula, no fue encontrado');
        this.formGroupFilter.reset();
        this.colorState = 'black';
      }
    });
  }

  searchModule(){
    this.moduloService.listModule().subscribe(resp => {
      this.dataSource = resp.data;
    });
  }

  mappingDataUser(usuario: TblUsuarios){
    this.formGroupFilter.controls['usuarioId'].setValue(usuario.id);
    this.formGroupFilter.controls['apellido'].setValue(usuario.apellidos);
    this.formGroupFilter.controls['nombre'].setValue(usuario.nombres);
    this.formGroupFilter.controls['nombreDeUsuario'].setValue(usuario.nombreDeUsuario);
    this.formGroupFilter.controls['id'].setValue(usuario.id);
    this.defineState(usuario.estadoId);
  }

  private defineState(stateId: number){
    if(UserState.Activo == stateId){
      this.colorState = 'green';
    }
    if(UserState.Inactivo == stateId){
      this.colorState = 'red';
    }
  }

  the_C_FieldChanged(item: any, selected: any){
    let temp: PermisoDTO = new PermisoDTO();
    if(selected.checked){
      //...Esta accion se va a disparar en caso de que el elemento se marque como true.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      temp.c = selected.checked;
      // temp.r = this.formGroupPermission.controls['r'].value;
      // temp.u = this.formGroupPermission.controls['u'].value;
      // temp.d = this.formGroupPermission.controls['d'].value;
      temp.r = (item.r != null) ? true : false;
      temp.u = (item.u != null) ? true : false;
      temp.d = (item.d != null) ? true : false;
      this.changesElementList(temp, this.userData, ElementToChanges.C);
    }
    else{
      //...Esta accion se va a disparar en caso de que el elemento se marque como falso.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      temp.c = selected.checked;
      // temp.r = this.formGroupPermission.controls['r'].value;
      // temp.u = this.formGroupPermission.controls['u'].value;
      // temp.d = this.formGroupPermission.controls['d'].value;
      temp.r = (item.r != null) ? true : false;
      temp.u = (item.u != null) ? true : false;
      temp.d = (item.d != null) ? true : false;
      this.changesElementList(temp, this.userData, ElementToChanges.C);
    }
  }

  the_R_FieldChanged(item: any, selected: any){
    let temp: PermisoDTO = new PermisoDTO();

    if(selected.checked){
      //...Esta accion se va a disparar en caso de que el elemento se marque como true.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      // temp.c = this.formGroupPermission.controls['c'].value;
      temp.c = (item.c != null) ? item.c : false;
      temp.r = selected.checked;
      // temp.u = this.formGroupPermission.controls['u'].value;
      // temp.d = this.formGroupPermission.controls['d'].value;
      temp.u = (item.u != null) ? item.u : false;
      temp.d = (item.d != null) ? item.d : false;
      this.changesElementList(temp, this.userData, ElementToChanges.R);
    }
    else{
      //...Esta accion se va a disparar en caso de que el elemento se marque como falso.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      // temp.c = this.formGroupPermission.controls['c'].value;
      temp.c = (item.c != null) ? item.c : false;
      temp.r = selected.checked;
      // temp.u = this.formGroupPermission.controls['u'].value;
      // temp.d = this.formGroupPermission.controls['d'].value;
      temp.u = (item.u != null) ? item.u : false;
      temp.d = (item.d != null) ? item.d : false;
      this.changesElementList(temp, this.userData, ElementToChanges.R);
    }
  }

  the_U_FieldChanged(item: any, selected: any){
    let temp: PermisoDTO = new PermisoDTO();

    if(selected.checked){
      //...Esta accion se va a disparar en caso de que el elemento se marque como true.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      // temp.c = this.formGroupPermission.controls['c'].value;
      // temp.r = this.formGroupPermission.controls['r'].value;
      temp.c = (item.c != null) ? item.c : false;
      temp.r = (item.r != null) ? item.r : false;
      temp.u = selected.checked;
      // // temp.d = this.formGroupPermission.controls['d'].value;
      temp.d = (item.d != null) ? item.d : false;
      this.changesElementList(temp, this.userData, ElementToChanges.U);
    }
    else{
      //...Esta accion se va a disparar en caso de que el elemento se marque como falso.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      // temp.c = this.formGroupPermission.controls['c'].value;
      // temp.r = this.formGroupPermission.controls['r'].value;
      temp.c = (item.c != null) ? item.c : false;
      temp.r = (item.r != null) ? item.r : false;
      temp.u = selected.checked;
      // // temp.d = this.formGroupPermission.controls['d'].value;
      temp.d = (item.d != null) ? item.d : false;
      this.changesElementList(temp, this.userData, ElementToChanges.U);
    }
  }

  the_D_FieldChanged(item: any, selected: any){
    let temp: PermisoDTO = new PermisoDTO();

    if(selected.checked){
      //...Esta accion se va a disparar en caso de que el elemento se marque como true.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      // temp.c = this.formGroupPermission.controls['c'].value;
      // temp.r = this.formGroupPermission.controls['r'].value;
      // temp.u = this.formGroupPermission.controls['u'].value;
      temp.c = (item.c != null) ? item.c : false;
      temp.r = (item.r != null) ? item.r : false;
      temp.u = (item.u != null) ? item.u : false;
      temp.d = selected.checked;
      this.changesElementList(temp, this.userData, ElementToChanges.D);
    }
    else{
      //...Esta accion se va a disparar en caso de que el elemento se marque como falso.
      temp.usuarioId = this.formGroupFilter.controls['usuarioId'].value;
      temp.rolId = this.formGroupFilter.controls['rolId'].value;
      temp.moduloId = item.id;
      // temp.c = this.formGroupPermission.controls['c'].value;
      // temp.r = this.formGroupPermission.controls['r'].value;
      // temp.u = this.formGroupPermission.controls['u'].value;
      temp.c = (item.c != null) ? item.c : false;
      temp.r = (item.r != null) ? item.r : false;
      temp.u = (item.u != null) ? item.u : false;
      temp.d = selected.checked;
      this.changesElementList(temp, this.userData, ElementToChanges.D);
    }
  }

  changesElementList(item: PermisoDTO, user: TblUsuarios, elementToChange: number = 0){
    if(this.listPermission.length > 0){
      let tempElement: any = this.listPermission.find(x => x.moduloId === item.moduloId);

      if(tempElement != null && elementToChange > 0){
        let tempList: Array<PermisoDTO> = new Array<PermisoDTO>();
        tempList = this.listPermission.map((element) => {
          if(item.moduloId === element.moduloId){
            element.usuarioId = user.id;
            element.rolId = user.rolId;
            element.moduloId = item.moduloId;
            switch(elementToChange){
              case 1:
                element.c = item.c;
                break;
              case 2:
                element.r = item.r;
                break;
              case 3:
                element.u = item.u;
                break;
              case 4:
                element.d = item.d;
                break;
            }
            return element;
          }
          return element;
        });
        this.listPermission = tempList;
      }
      else{
        this.listPermission.push(item);
      }
    }else{
      this.listPermission.push(item);
    }
  }
  addElementExistToList(item: PermisoDTO){
    let temp: TblModuloDTO = new TblModuloDTO();
    let tempList: Array<TblModuloDTO> = this.dataSource.map((element: any) => {
      if(element.id == item.moduloId){
        temp = element;
        temp.c = item.c;
        temp.r = item.r;
        temp.u = item.u;
        temp.d = item.d;
        return temp;
      }
      return element;
    });
    // this.dataSource = [...tempList];
    this.dataSource = tempList;
  }

  btnSavePermission(){
    if(this.listPermission.length > 0 && this.listPermission[0].usuarioId > 0){
      console.log(this.listPermission);

      this.permisoService.editarYAgregarPermisosExistentes(this.listPermission).subscribe(resp => {
        if(resp.succcess){
          this.sweetAlert.successMessage(resp.message);
        }
        else{
          this.sweetAlert.dangerMessage(resp.message);
        }
      });
    }
    else{
      let seNecesitaSeleccionarLosPermisos: boolean = (this.listPermission.length < 0) ? true : false;
      let seNecesitaIndicarElUsuario: boolean = (this.formGroupFilter.controls['usuarioId'].value <= 0) ? true : false;

      if(seNecesitaSeleccionarLosPermisos){
        this.sweetAlert.infoMessage("Necesita seleccionar los permisos del usuario antes de intentar guardarlo.");
      }
      if(seNecesitaIndicarElUsuario){
        this.sweetAlert.infoMessage("Necesita seleccionar los permisos del usuario antes de intentar guardarlo.");
      }
    }
  }
}
