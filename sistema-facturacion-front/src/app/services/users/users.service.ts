import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  datosDePrueba(param: any): Observable<any>{
    return this.http.get<any>('https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/orders', param);
  }
}
