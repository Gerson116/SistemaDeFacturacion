import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TblModuloDTO } from 'src/app/models/dtos/modulo-dto';
import { ModuloService } from 'src/app/services/modulo/modulo.service';
import { SweetalertService } from 'src/app/services/sweetalert2/sweetalert.service';

@Component({
  selector: 'app-module-maintenance',
  templateUrl: './module-maintenance.component.html',
  styleUrls: ['./module-maintenance.component.css', '../../shared/css/dashboard-start.css']
})
export class ModuleMaintenanceComponent {

  title: string = 'Mantenimiento Modulo';
  formGroupModuleMaintenance: FormGroup | any;
  dataSources: any;
  tblModuloDTO: TblModuloDTO;

  constructor(private formBuilder: FormBuilder,
    private moduleServices: ModuloService,
    private sweetAlertServices: SweetalertService){
    this.tblModuloDTO = new TblModuloDTO();
  }

  ngOnInit(){
    this.buildForm();
    this.searchModules();
  }

  buildForm(){
    this.formGroupModuleMaintenance = this.formBuilder.group({
      nombreDelModulo: ['', Validators.required],
      rutaDelModulo: ['', Validators.required]
    });
  }

  searchModules(){
    //...
    this.moduleServices.listModule().subscribe(resp => {
      this.dataSources = resp.data
    });
  }

  mappingData(): TblModuloDTO {
    let temp: TblModuloDTO = new TblModuloDTO();
    temp.nombre = this.formGroupModuleMaintenance.controls['nombreDelModulo'].value;
    temp.ruta = this.formGroupModuleMaintenance.controls['rutaDelModulo'].value;
    return temp;
  }

  btnAddModule(){
    if(this.formGroupModuleMaintenance.valid){
      //...
      this.tblModuloDTO = this.mappingData();
      this.moduleServices.postNuevoModulo(this.tblModuloDTO).subscribe(resp => {
        if(resp.succcess){
          this.searchModules();
          this.sweetAlertServices.successMessageRight('Exito');
        }
      })
    }else{
      this.sweetAlertServices.infoMessage('No se aceptan campos nulos');
    }
  }

  btnDeleteElement(itemId: number){
    this.moduleServices.DeleteEliminarModulo(itemId).subscribe(resp => {
      if(resp.succcess){
        this.searchModules();
        this.sweetAlertServices.successMessageRight('Exito');
      }
    });
  }
}
