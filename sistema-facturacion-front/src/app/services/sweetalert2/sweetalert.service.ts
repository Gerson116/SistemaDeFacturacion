import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SweetalertService {

  constructor() { }

  successMessage(message: string){
    Swal.fire({
      icon: 'success',
      title: "Exito",
      text: message
    });
  }
  successMessageRight(title: string){
    Swal.fire({
      position: 'top-end',
      icon: 'success',
      title: title,
      showConfirmButton: false,
      timer: 1500
    })
  }
  infoMessage(message: string){
    Swal.fire({
      icon: 'warning',
      title: "Advertencia",
      text: message
    });
  }
  dangerMessage(message: string){
    Swal.fire({
      icon: 'error',
      title: "Ocurrio un error",
      text: message
    });
  }
  confirmationMessage(title: string, message: string){}
}
