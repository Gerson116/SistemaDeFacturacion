import { Component } from '@angular/core';
import ArrayStore from 'devextreme/data/array_store';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent {

  role: Array<any> = [
    { ID: 1, Name: 'Admin' },
    { ID: 2, Name: 'Usuario' }
  ];
  estado: Array<any> = [
    { ID: 1, Name: 'Activo' },
    { ID: 2, Name: 'Inactivo' }
  ];

  maxDate: Date = new Date();
  data: any;
  dataEstado: any;

  constructor(){
    this.data = new ArrayStore({
      data: this.role,
      key: 'ID'
    });
    this.dataEstado = new ArrayStore({
      data: this.estado,
      key: 'ID'
    });
  }
}
