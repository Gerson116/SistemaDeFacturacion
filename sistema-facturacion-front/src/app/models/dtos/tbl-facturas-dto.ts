
export class TblFacturasDTO{

  id: number;
  empresaId: number;
  usuarioId: number;
  fechaDeCreacion: Date;
  lineaDePago: string;
  subTotal: number;
  descuento: number;
  iva: number;
  totalPagado: number;
  formaDePagoId: number;

  constructor(){
    this.id = 0;
    this.empresaId = 0;
    this.usuarioId = 0;
    this.fechaDeCreacion = new Date();
    this.lineaDePago = '';
    this.subTotal = 0;
    this.descuento = 0;
    this.iva = 0;
    this.totalPagado = 0;
    this.formaDePagoId = 0;
  }
}
