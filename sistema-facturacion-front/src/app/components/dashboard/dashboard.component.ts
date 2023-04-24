import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  continentsMenu: any;
  isDrawerOpened: boolean;

  constructor(){
    this.continentsMenu = [
      {
        id: '1',
        text: 'Perfil',
        expanded: false
      },
      {
        id: '2',
        text: 'Usuarios',
        expanded: true,
        items: [
          {
            id: '2.1',
            text: 'Listar Usuarios'
          },
          {
            id: '2.2',
            text: 'Agregar Usuarios'
          }
        ]
      },
      {
        id: '3',
        text: 'Rol',
        expanded: true,
        items: [
          {
            id: '3.1',
            text: 'Listar Rol'
          },
          {
            id: '3.2',
            text: 'Agregar Rol'
          }
        ]
      },
      {
        id: '4',
        text: 'Permiso',
        expanded: true,
        items: [
          {
            id: '4.1',
            text: 'Listar Permiso'
          },
          {
            id: '4.2',
            text: 'Agregar Permiso'
          }
        ]
      },
      {
        id: '5',
        text: 'Ventas',
        expanded: true,
        items: [
          {
            id: '5.1',
            text: 'Listar Ventas'
          },
          {
            id: '5.2',
            text: 'Agregar Venta'
          }
        ]
      }
    ]
    this.isDrawerOpened = false;
  }
}
