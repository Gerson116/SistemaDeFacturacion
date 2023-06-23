import { Pager } from "./pager";

export class OperationResultRequest{
  succcess: boolean;
  message: string;
  data: any;
  paginacion: Pager | any;
  token: string;
  fechaExpiracion: Date;

  constructor(){
    this.succcess = false;
    this.message = "";
    this.token = "";
    this.fechaExpiracion = new Date();
  }
}
