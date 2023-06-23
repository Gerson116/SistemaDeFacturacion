
export class TblModuloDTO{
  id: number;
  nombre: string;
  ruta: string;
  c: boolean;
  r: boolean;
  u: boolean;
  d: boolean;

  constructor(){
    this.id = 0;
    this.nombre = '';
    this.ruta = '';
    this.c = false;
    this.r = false;
    this.u = false;
    this.d = false;
  }
}
