import { Component, EventEmitter, Input, Output } from '@angular/core';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
import { NuevaFacturaDTO } from 'src/app/models/dtos/nueva-factura-dto';
import { TblDetalleDeFacturasDTO } from 'src/app/models/dtos/tbl-detalle-de-facturas-dto';
import { TblFacturasDTO } from 'src/app/models/dtos/tbl-facturas-dto';
import { SalesService } from 'src/app/services/sales/sales.service';
import { SweetalertService } from 'src/app/services/sweetalert2/sweetalert.service';
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-pdf-factura',
  templateUrl: './pdf-factura.component.html',
  styleUrls: ['./pdf-factura.component.css']
})
export class PdfFacturaComponent {

  //... Estos inputs se utilizan para mostrar el boton imprimir en el listado de facturas.
  @Input() facturaId: number = 0;
  @Input() btnVisualizarFactura: boolean = false;
  //... Estos inputs se utilizan para mostrar el boton imprimir en el listado de facturas.

  //... Estos inputs se utilizan para mostrar el boton generar factura y generar la factura
  @Input() cabezera: TblFacturasDTO = new TblFacturasDTO();
  @Input() detalleFactura: Array<TblDetalleDeFacturasDTO> = new Array<TblDetalleDeFacturasDTO>();
  @Input() btnVisualizarGenerarFactura: boolean = false;
  //... Estos inputs se utilizan para mostrar el boton generar factura y generar la factura

  objNuevaFactura: NuevaFacturaDTO;
  objFactura: TblFacturasDTO;
  listDetalleFactura: Array<TblDetalleDeFacturasDTO>;

  constructor(private salesService: SalesService,
              private alert: SweetalertService){
    this.objFactura = new TblFacturasDTO();
    this.listDetalleFactura = new Array<TblDetalleDeFacturasDTO>();
    this.objNuevaFactura = new NuevaFacturaDTO();
  }

  btnImprimirFacturaExistente(){
    //... Para culminar este metodo, debo buscar la manera de como presentar una factura y presentarla en PDF
    this.salesService.getFactura(this.facturaId).subscribe(resp => {
      //...Buscar la cabecera
      this.objNuevaFactura = resp.data;
      this.generarPdf(this.objNuevaFactura);
    });
  }

  btnGenerarFactura(){
    //... Este boton se encarga de generar factura.
    if(this.cabezera != null && this.detalleFactura != null && this.detalleFactura.length > 0){
      this.objNuevaFactura.factura = this.cabezera;
      this.objNuevaFactura.detalleFactura = this.detalleFactura;
      this.salesService.postNuevoFactura(this.objNuevaFactura).subscribe(resp => {
        if(resp.succcess){
          this.objNuevaFactura.factura.id = resp.data.FacturaId;
          this.objNuevaFactura.factura.lineaDePago = resp.data.LineaDeFactura;
          this.generarPdf(this.objNuevaFactura);
          this.alert.successMessage(`La factura ${resp.data.FacturaId} se genero con exito.`);
        }else{
          this.alert.dangerMessage('Ocurrio un error al intentar generar la factura.');
        }
      });
      console.log('Los datos no son nulos.');
    }else{
      console.log('Los datos son nulos');
      this.alert.infoMessage('Debe seleccionar por lo menos un articulo para poder generar la factura.');
    }
  }

  generarPdf(objNuevaFactura: NuevaFacturaDTO){
    //...
    let bodyProducts: any = [];
    bodyProducts.push(['Nombre Producto', 'Precio Unidad', 'Cantidad']);
    objNuevaFactura.detalleFactura.forEach(dato => {
      bodyProducts.push([dato.nombreProducto, dato.precioUnidad, dato.cantidadProducto]);
    });

    const pdfDefinition: any = {
      content: [
        {
          text: `Factura Final`,
          style: 'header'
        },
        {
          alignment: 'justify',
          columns: [
            {
              text: `Fecha de Factura: ${new Date(objNuevaFactura.factura.fechaDeCreacion).toLocaleDateString()}`
            },
            {
              text: `No. Factura: ${objNuevaFactura.factura.id}`
            }
          ]
        },
        {
          alignment: 'justify',
          columns: [
            {
              text: `Linea de Factura: ${objNuevaFactura.factura.lineaDePago}`
            }
          ]
        },
        {
          text: `Detalle Factura`,
          style: 'header'
        },
        {
          style: 'tableExample',
          table: {
            body: bodyProducts
          }
        },
        {
          text: `IVA: $${objNuevaFactura.factura.iva}`
        },
        {
          text: `Sub Total Pagado: $${objNuevaFactura.factura.subTotal}`
        },
        {
          text: `Total Pagado: $${objNuevaFactura.factura.totalPagado}`
        }
      ],
      styles: {
        header: {
          fontSize: 18,
          bold: true,
          margin: [0, 0, 0, 10]
        },
        subheader: {
          fontSize: 16,
          bold: true,
          margin: [0, 10, 0, 5]
        },
        tableExample: {
          margin: [0, 5, 0, 15]
        },
        tableHeader: {
          bold: true,
          fontSize: 13,
          color: 'black'
        }
      },
      defaultStyle: {
        // alignment: 'justify'
      }
    };

    pdfMake.createPdf(pdfDefinition).open();
  }

}
