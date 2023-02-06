using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tienda.Models
{
    public class Usuario
    {
        public Usuario()
        {
        }

        public Usuario(int id, string rfc, string username, string password, string nombre, string appaterno, string apmaterno, string edad, string sexo, Direccion direccion, Contacto contacto, DateTime fechaRegistro)
        {
            this.id = id;
            this.rfc = rfc;
            this.username = username;
            this.password = password;
            this.nombre = nombre;
            this.appaterno = appaterno;
            this.apmaterno = apmaterno;
            this.edad = edad;
            this.sexo = sexo;
            this.direccion = direccion;
            this.contacto = contacto;
            this.fechaRegistro = fechaRegistro;
        }


        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [StringLength(13)]
        [DisplayName("RFC")]
        public string rfc { get; set; }

        [Required]
        [DisplayName("Usuario")]
        [StringLength(20)]
        public string username { get; set; }

        [Required]
        [DisplayName("Contraseña")]
        [StringLength(20)]
        public string password { get; set; }

        [Required]
        [DisplayName("Nombre")]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [DisplayName("Apellido Paterno")]
        [StringLength(50)]
        public string appaterno { get; set; }

        [Required]
        [DisplayName("Apellido Materno")]
        [StringLength(50)]
        public string apmaterno { get; set; }

        [StringLength(3)]
        [Range(1, 130, ErrorMessage = "Escribe una edad valida")]
        [DisplayName("Edad")]
        public string edad { get; set; }

        [StringLength(1)]
        [DisplayName("Sexo")]
        public string sexo { get; set; }

        public Direccion direccion { get; set; }

        public Contacto contacto { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime fechaRegistro { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Usuario usuario &&
                   id == usuario.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}