
export class TblDetalleDeFacturasDTO{

  // id: number;
  facturaId: number;
  productoId: number;
  nombreProducto: string;
  precioUnidad: number;
  subTotal: number;
  descuento: number;
  iva: number;
  totalPagado: number;
  formaDePagoId: number;
  cantidadProducto: number;
  // fechaDeRegistro: Date;

  constructor(){
    // this.id = 0;
    this.facturaId = 0;
    this.productoId = 0;
    this.nombreProducto = '';
    this.precioUnidad = 0;
    this.subTotal = 0;
    this.descuento = 0;
    this.iva = 0;
    this.totalPagado = 0;
    this.formaDePagoId = 0;
    this.cantidadProducto = 0;
    // this.fechaDeRegistro = new Date();
  }
}
