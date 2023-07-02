import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { parseNumber } from 'devextreme/localization';
import { TblFacturasDTO } from 'src/app/models/dtos/tbl-facturas-dto';
import { TblUsuarios } from 'src/app/models/tbl-usuarios';
import { SalesService } from 'src/app/services/sales/sales.service';
import { SweetalertService } from 'src/app/services/sweetalert2/sweetalert.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-sales-list',
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.css', '../../../shared/css/dashboard-start.css']
})
export class SalesListComponent {

  title: string = 'Nueva Venta';
  listSales: Array<TblFacturasDTO>;
  usuarioObj: TblUsuarios = JSON.parse(JSON.stringify(localStorage.getItem(environment.usuario)));
  empresaId: number | any;
  filterFormGroup: FormGroup | any;

  constructor(private salesService: SalesService,
    private alerts: SweetalertService,
    private formBuilder: FormBuilder){
    //...
    this.listSales = new Array<TblFacturasDTO>();
    this.empresaId = (localStorage.getItem(environment.empresaId) == null) ? 10 : localStorage.getItem(environment.empresaId);
  }

  ngOnInit(){
    this.builderForm();
    this.searchSales();
  }

  builderForm(){
    this.filterFormGroup = this.formBuilder.group({
      facturaId: [0, Validators.required]
    });
  }

  searchSales(pagina: number = 1, cantidad: number = 10){
    //...
    this.salesService.getAllFactura(this.empresaId).subscribe(resp => {
      this.listSales = resp.data;
    });
  }

  searchSalesPerID(){
    let facturaId: number = this.filterFormGroup.controls['facturaId'].value;
    if (facturaId == 0) {
      this.searchSales();
    }
    else{
      this.salesService.buscarFactura(facturaId).subscribe(resp => {
        this.listSales = resp.data;
      });
    }
  }

  btnEliminar(facturaId: number){
    //... Para culminar este metodo, debo agregar una ventana de alerta donde se le indique al usuario si esta
    //... claro de lo que hara
    this.alerts.confirmationMessage(`La factura #${facturaId}`);

    this.alerts.actionRealizada$.subscribe(respAction => {
      console.log(respAction);
      if (respAction) {
        this.salesService.deleteFactura(this.empresaId, facturaId).subscribe(resp => {
          //... Se aplicaron los cambios.
          this.searchSales();
        });
      }
    });
  }
}
