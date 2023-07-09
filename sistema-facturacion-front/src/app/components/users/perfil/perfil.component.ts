import { Component } from '@angular/core';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent {

  totalDeLasVentas: number = 500;
  detalleDeVentasSemanal: any;

  constructor(){
    this.detalleDeVentasSemanal = [
      { day: 'Lunes', oranges: 1 },
      { day: 'Martes', oranges: 2 },
      { day: 'Miercoles', oranges: 3 },
      { day: 'Jueves', oranges: 5 },
      { day: 'Viernes', oranges: 5 },
      { day: 'Sabado', oranges: 3 },
      { day: 'Domingo', oranges: 8 },
    ];
  }

  ngOnInit(){
  }


  pointClickHandler(e: any) {
    this.toggleVisibility(e.target);
  }

  legendClickHandler(e: any) {
    const arg = e.target;
    const item = e.component.getAllSeries()[0].getPointsByArg(arg)[0];

    this.toggleVisibility(item);
  }

  toggleVisibility(item: any) {
    if (item.isVisible()) {
      item.hide();
    } else {
      item.show();
    }
  }

}
