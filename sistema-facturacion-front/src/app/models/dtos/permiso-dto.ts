
export class PermisoDTO{

  usuarioId: number;
  rolId: number;
  moduloId: number;
  c: boolean;
  r: boolean;
  u: boolean;
  d: boolean;

  constructor(){
    this.usuarioId = 0;
    this.rolId = 0;
    this.moduloId = 0;
    this.c = false;
    this.r = false;
    this.u = false;
    this.d = false;

  }
}
