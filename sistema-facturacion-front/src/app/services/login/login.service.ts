import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CambiarPasswordOlvidado } from 'src/app/models/dtos/cambiar-password-olvidado';
import { IniciarSesion } from 'src/app/models/dtos/iniciar-sesion';
import { OperationResultRequest } from 'src/app/models/dtos/operation-result-request';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  baseUrl: string = `${environment.api_url}ManejoDeSesion/`;

  constructor(private http: HttpClient) { }

  iniciarSesion(element: IniciarSesion): Observable<OperationResultRequest>{
    return this.http.post<OperationResultRequest>(`${this.baseUrl}IniciarSesion`, element);
  }

  olvideMiPass(element: CambiarPasswordOlvidado): Observable<OperationResultRequest>{
    return this.http.post<any>(`${this.baseUrl}OlvideMiPass`, element);
  }
}
