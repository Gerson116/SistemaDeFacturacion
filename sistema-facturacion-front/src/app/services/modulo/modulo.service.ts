import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TblModuloDTO } from 'src/app/models/dtos/modulo-dto';
import { OperationResultRequest } from 'src/app/models/dtos/operation-result-request';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ModuloService {

  baseURL: string = `${environment.api_url}Modulo/`;

  constructor(private http: HttpClient) { }

  listModule(): Observable<OperationResultRequest>{
    return this.http.get<OperationResultRequest>(`${this.baseURL}GetAllModulos`);
  }

  postNuevoModulo(item: TblModuloDTO): Observable<OperationResultRequest>{
    return this.http.post<OperationResultRequest>(`${this.baseURL}PostNuevoModulo`, item);
  }

  DeleteEliminarModulo(itemId: number){
    return this.http.delete<OperationResultRequest>(`${this.baseURL}DeleteEliminarModulo/${itemId}`);
  }
}
