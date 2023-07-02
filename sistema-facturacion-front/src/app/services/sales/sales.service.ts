import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NuevaFacturaDTO } from 'src/app/models/dtos/nueva-factura-dto';
import { OperationResultRequest } from 'src/app/models/dtos/operation-result-request';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  baseUrl: string = `${environment.api_url}Factura/`;

  constructor(private http: HttpClient) { }

  postAddPermission(item: NuevaFacturaDTO[]): Observable<OperationResultRequest>{
    return this.http.post<OperationResultRequest>(`${this.baseUrl}PostAgregarFactura`, item);
  }

  buscarFactura(facturaId: number): Observable<OperationResultRequest>{
    return this.http.get<OperationResultRequest>(`${this.baseUrl}BuscarFactura/${facturaId}`);
  }

  getAllFactura(empresaId: number): Observable<OperationResultRequest>{
    return this.http.get<OperationResultRequest>(`${this.baseUrl}GetAllFactura/${empresaId}`);
  }

  getFactura(facturaId: number){
    return this.http.get<OperationResultRequest>(`${this.baseUrl}GetFactura/${facturaId}`);
  }

  getCabeceraFactura(facturaId: number){
    return this.http.get<OperationResultRequest>(`${this.baseUrl}GetCabeceraFactura/${facturaId}`);
  }

  getDetalleFactura(facturaId: number){
    return this.http.get<OperationResultRequest>(`${this.baseUrl}GetDetalleFactura/${facturaId}`);
  }

  deleteFactura(empresaId:number, facturaId: number): Observable<OperationResultRequest>{
    return this.http.delete<OperationResultRequest>(`${this.baseUrl}DeleteFactura/${empresaId}/${facturaId}`);
  }
}
