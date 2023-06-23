import { ParametrosDeBusqueda } from "./parametros-de-busqueda";


export class FiltroUsuario extends ParametrosDeBusqueda{
  Identificacion: string;
  Pasaporte: string;

  constructor() {
    super();
    this.Identificacion = '';
    this.Pasaporte = '';
  }
}
