using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_facturacion_api.Data.DTOs
{
    public class TblUsuariosDTO
    {
        public TblUsuariosDTO()
        {
            this.Empresas = new HashSet<TblEmpresas>();
        }
        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string NombreDeUsuario { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string? TarjetaDeIdentificacion { get; set; }
        public string? Pasaporte { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
        public int EstadoId { get; set; }
        public virtual TblEstado? Estado { get; set; }
        public virtual ICollection<TblEmpresas>? Empresas { get; set; }
    }
}
