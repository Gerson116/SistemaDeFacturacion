import { Pager } from "./pager";

export class OperationResultRequest{
  succcess: boolean;
  message: string;
  data: any;
  paginacion: Pager | any;

  constructor(){
    this.succcess = false;
    this.message = "";
  }
}
