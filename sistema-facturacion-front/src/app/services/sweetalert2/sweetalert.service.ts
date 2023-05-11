import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SweetalertService {

  constructor() { }

  successMessage(title: string, message: string){
    Swal.fire({
      icon: 'success',
      title: title,
      text: message
    });
  }
  infoMessage(title: string, message: string){
    Swal.fire({
      icon: 'warning',
      title: title,
      text: message
    });
  }
  dangerMessage(title: string, message: string){
    Swal.fire({
      icon: 'error',
      title: title,
      text: message
    });
  }
  confirmationMessage(title: string, message: string){}
}
