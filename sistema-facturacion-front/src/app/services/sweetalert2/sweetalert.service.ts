import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SweetalertService {

  actionRealizada$ = new Subject<boolean>();

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
  confirmationMessage(message: string){

    let actionResult: boolean = false;

    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
      title: 'Esta seguro que desea realizar esta acción?',
      text: "Una vez realice la misma, no podra revertirlo",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, eliminar',
      cancelButtonText: 'No, cancelar!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        swalWithBootstrapButtons.fire(
          'Eliminado!',
          `${message} fue eliminado(a) con exito.`,
          'success'
        );
        actionResult = result.isConfirmed;
        this.actionRealizada$.next(result.isConfirmed);
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire(
          'Cancelado!',
          `${message} no fue eliminado(a)`,
          'error'
        );

        actionResult = result.isConfirmed;
        this.actionRealizada$.next(result.isConfirmed);
      }
    });
  }
}
