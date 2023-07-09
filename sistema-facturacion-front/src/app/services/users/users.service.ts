import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationResultRequest } from 'src/app/models/dtos/operation-result-request';
import { ParametrosDeBusqueda } from 'src/app/models/dtos/parametros-de-busqueda';
import { TblUsuarios } from 'src/app/models/tbl-usuarios';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl: string = `${environment.api_url}Usuario/`;

  constructor(private http: HttpClient) { }

  getUserId(usuarioId: number): Observable<OperationResultRequest>{
    return this.http.get<OperationResultRequest>(`${this.baseUrl}PerfilUsuario/${usuarioId}`);
  }

  userList(pagina: number = 1, cantidadDePagina: number = 10): Observable<OperationResultRequest>{
    return this.http.get<OperationResultRequest>(`${this.baseUrl}ListadoDeUsuarios/${pagina}/${cantidadDePagina}`);
  }

  filterUser(item: ParametrosDeBusqueda): Observable<OperationResultRequest>{
    return this.http.post<OperationResultRequest>(`${this.baseUrl}BuscarUsuarios`, item);
  }

  filterUserPerId(cedula: string): Observable<OperationResultRequest>{
    return this.http.get<OperationResultRequest>(`${this.baseUrl}BuscarUsuariosPorCedula/${cedula}`);
  }

  saveUser(item: TblUsuarios): Observable<OperationResultRequest>{
    return this.http.post<OperationResultRequest>(`${this.baseUrl}AgregarUsuario`, item);
  }

  editUser(item: TblUsuarios): Observable<OperationResultRequest>{
    return this.http.put<OperationResultRequest>(`${this.baseUrl}EditarUsuario`, item);
  }

  changeUserState(userId: number, state: number): Observable<OperationResultRequest>{
    let replace = [
      {
        "path": "EstadoId",
        "op": "replace",
        "value": state
      }
    ];
    return this.http.patch<OperationResultRequest>(`${this.baseUrl}CambiarEstadoUsuario/${userId}`, replace);
  }
}
