
export class TblRol{

  Id: number;
  Nombre: string;
  FechaDeCreacion: Date;

  constructor(){
    this.Id = 0;
    this.Nombre = '';
    this.FechaDeCreacion = new Date();
  }
}
