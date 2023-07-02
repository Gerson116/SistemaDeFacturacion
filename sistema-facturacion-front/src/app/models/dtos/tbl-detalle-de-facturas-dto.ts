
export class TblDetalleDeFacturasDTO{

  // id: number;
  facturaId: number;
  productoId: number;
  nombreProducto: string;
  precioUnidad: number;
  cantidadProducto: number;
  // fechaDeRegistro: Date;

  constructor(){
    // this.id = 0;
    this.facturaId = 0;
    this.productoId = 0;
    this.nombreProducto = '';
    this.precioUnidad = 0;
    this.cantidadProducto = 0;
    // this.fechaDeRegistro = new Date();
  }
}
