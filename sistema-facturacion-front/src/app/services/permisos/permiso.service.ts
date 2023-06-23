import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationResultRequest } from 'src/app/models/dtos/operation-result-request';
import { PermisoDTO } from 'src/app/models/dtos/permiso-dto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PermisoService {

  baseUrl: string = `${environment.api_url}Permisos/`;

  constructor(private http: HttpClient) { }

  postAddPermission(item: PermisoDTO[]): Observable<OperationResultRequest>{
    return this.http.post<OperationResultRequest>(`${this.baseUrl}PostAgregarPermisos`, item);
  }

  editarYAgregarPermisosExistentes(item: PermisoDTO[]): Observable<OperationResultRequest>{
    return this.http.put<OperationResultRequest>(`${this.baseUrl}EditarYAgregarPermisosExistentes`, item);
  }

  getAllPermisos(usuarioId: number): Observable<OperationResultRequest>{
    return this.http.get<OperationResultRequest>(`${this.baseUrl}GetAllPermisos/${usuarioId}`);
  }
}
