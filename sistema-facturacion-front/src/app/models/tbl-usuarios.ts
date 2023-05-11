
export class TblUsuarios{

  id: number;
  apellidos: string;
  nombres: string;
  nombreDeUsuario: string;
  fechaDeNacimiento: Date;
  tarjetaDeIdentificacion: string;
  pasaporte: string;
  email: string;
  password: string;
  rolId: number;
  estadoId: number;

  constructor(){
    this.id = 0;
    this.apellidos = '';
    this.nombres = '';
    this.nombreDeUsuario = '';
    this.fechaDeNacimiento = new Date();
    this.tarjetaDeIdentificacion = '';
    this.pasaporte = '';
    this.email = '';
    this.password = '';
    this.rolId = 0;
    this.estadoId = 0;
  }
}
