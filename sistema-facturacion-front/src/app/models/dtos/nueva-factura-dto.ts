import { TblDetalleDeFacturasDTO } from "./tbl-detalle-de-facturas-dto";
import { TblFacturasDTO } from "./tbl-facturas-dto";


export class NuevaFacturaDTO{
  factura: TblFacturasDTO;
  detalleFactura: Array<TblDetalleDeFacturasDTO>;

  constructor(){
    this.factura = new TblFacturasDTO();
    this.detalleFactura = new Array<TblDetalleDeFacturasDTO>();
  }
}
