import { Component, Input } from '@angular/core';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
import { NuevaFacturaDTO } from 'src/app/models/dtos/nueva-factura-dto';
import { TblDetalleDeFacturasDTO } from 'src/app/models/dtos/tbl-detalle-de-facturas-dto';
import { TblFacturasDTO } from 'src/app/models/dtos/tbl-facturas-dto';
import { SalesService } from 'src/app/services/sales/sales.service';
pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-pdf-factura',
  templateUrl: './pdf-factura.component.html',
  styleUrls: ['./pdf-factura.component.css']
})
export class PdfFacturaComponent {

  @Input() facturaId: number;
  objNuevaFactura: NuevaFacturaDTO;
  objFactura: TblFacturasDTO;
  listDetalleFactura: Array<TblDetalleDeFacturasDTO>;

  constructor(private salesService: SalesService){
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
