import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ParametrosDeBusqueda } from 'src/app/models/dtos/parametros-de-busqueda';
import { TblDetalleDeFacturasDTO } from 'src/app/models/dtos/tbl-detalle-de-facturas-dto';
import { TblFacturasDTO } from 'src/app/models/dtos/tbl-facturas-dto';
import { TblProducto } from 'src/app/models/dtos/tbl-producto';
import { TblUsuarios } from 'src/app/models/tbl-usuarios';
import { SalesService } from 'src/app/services/sales/sales.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-sales',
  templateUrl: './add-sales.component.html',
  styleUrls: ['./add-sales.component.css', '../../../shared/css/dashboard-start.css']
})
export class AddSalesComponent {

  elementosAgregados: Array<any>;
  formaDePagoFormGroup: FormGroup;
  productoFormGroup: FormGroup;
  mostrarAutoComplete: boolean = true;
  objProducto: TblProducto;
  producto: string;

  formaDePago: Array<any> = [
    { viewValue: 'Forma de Pago', value: 0 },
    { viewValue: 'Efectivo', value: 1 },
    { viewValue: 'Tarjeta de Credito', value: 2 },
    { viewValue: 'Cheque', value: 3 },
    { viewValue: 'Transferencia', value: 4 }
  ];
  subTotal: number;
  descuento: number;
  iva: number;
  totalPagado: number;

  sourcesProduct: Array<TblProducto>;
  cabezeraFactura: TblFacturasDTO;
  listDetalleFactura: Array<TblDetalleDeFacturasDTO>;
  btnVisualizarGenerarFactura: boolean = true;

  constructor(private formBuilder: FormBuilder,
              private salesService: SalesService){
    //...
    this.objProducto = new TblProducto();
    this.listDetalleFactura = new Array<TblDetalleDeFacturasDTO>();
    this.subTotal = 0;
    this.iva = 0;
    this.totalPagado = 0;
    this.cabezeraFactura = new TblFacturasDTO();
  }

  ngOnInit(){
    //...
    this.construirFormulario();
  }

  construirFormulario(){
    //...
    this.formaDePagoFormGroup = this.formBuilder.group({
      formaDePagoId: [0, Validators.required]
    });

    this.productoFormGroup = this.formBuilder.group({
      producto: ['', Validators.required],
      cantidadDeProducto: [0, Validators.required],
      precioProducto: [0, Validators.required],
      descuento: [0, Validators.required]
    });
  }

  buscarProducto(){
    //... Este metodo se utiliza para buscar los productos
    this.mostrarAutoComplete = true;
    this.producto = this.productoFormGroup.controls['producto'].value;

    let filtro: ParametrosDeBusqueda = new ParametrosDeBusqueda();
    filtro.nombre = this.producto;
    this.salesService.postBuscarFactura(filtro).subscribe(resp => {
      this.sourcesProduct = resp.data;
    });
  }

  seleccionarElemento(item: TblProducto){
    //... Este método se dispara al momento de hacer click.
    this.productoFormGroup.controls['producto'].setValue(item.nombre);
    this.objProducto = item;
    this.mostrarAutoComplete = false;
  }

  limpiarFormulario(){
    this.productoFormGroup.controls['producto'].setValue('');
    this.productoFormGroup.controls['cantidadDeProducto'].setValue(0);
    this.productoFormGroup.controls['precioProducto'].setValue(0);
  }

  adicionarElemento(){
    //... Al hacer click en el boton agregar, se debe agregar el detalle a la lista
    if (this.productoFormGroup.valid) {

      let detalleFactura: TblDetalleDeFacturasDTO = new TblDetalleDeFacturasDTO();
      detalleFactura.productoId = this.objProducto.id;
      detalleFactura.nombreProducto = this.objProducto.nombre;
      detalleFactura.precioUnidad = this.productoFormGroup.controls['precioProducto'].value;
      detalleFactura.cantidadProducto = this.productoFormGroup.controls['cantidadDeProducto'].value;
      detalleFactura.descuento = this.productoFormGroup.controls['descuento'].value;
      let subTotal: number = this.sumarSubTotal();
      let total: number = this.sumarTotal();
      let iva = this.sumarIVA(subTotal);
      detalleFactura.subTotal = subTotal;
      detalleFactura.iva = iva;
      this.listDetalleFactura.push(detalleFactura);

      this.subTotal += subTotal;
      this.descuento += detalleFactura.descuento;
      this.totalPagado += total;
      this.iva += iva;

      let cabecera: TblFacturasDTO = new TblFacturasDTO();
      cabecera.id = 0;
      cabecera.empresaId = parseInt(localStorage.getItem(environment.empresaId));
      let usuario: TblUsuarios = JSON.parse(localStorage.getItem(environment.usuario));
      cabecera.usuarioId = usuario.id;
      cabecera.subTotal = this.subTotal;
      cabecera.descuento = this.descuento;
      cabecera.iva = this.iva;
      cabecera.totalPagado = this.totalPagado;
      cabecera.formaDePagoId = this.formaDePagoFormGroup.controls['formaDePagoId'].value;
      this.cabezeraFactura = cabecera;

      this.limpiarFormulario();
    }
  }

  sumarSubTotal(): number{
    let sumaDeTotales: number = this.productoFormGroup.controls['precioProducto'].value * this.productoFormGroup.controls['cantidadDeProducto'].value;
    return sumaDeTotales;
  }

  sumarTotal(): number{
    let sumaDeTotales: number = this.productoFormGroup.controls['precioProducto'].value * this.productoFormGroup.controls['cantidadDeProducto'].value;
    // let iva: number = (sumaDeTotales * 18)/100;
    let total: number = sumaDeTotales + this.sumarIVA(sumaDeTotales);
    return total;
  }

  sumarIVA(subTotal: number, iva: number = 18): number{
    let ivaAplicado: number = (subTotal * iva) / 100;
    return ivaAplicado;
  }

  generarFactura(){
    //...
  }
}
