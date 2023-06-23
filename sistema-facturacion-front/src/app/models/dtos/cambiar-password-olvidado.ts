

export class CambiarPasswordOlvidado{
  email: string;
  newPassword: string;
  confirmPassword: string;

  constructor(){
    this.email = '';
    this.newPassword = '';
    this.confirmPassword = '';
  }
}
