using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tienda.Models
{
    public class Contacto
    {
        public Contacto()
        {
        }

        public Contacto(int id, string telefono1, string telefono2, string correo, int entidad)
        {
            this.id = id;
            this.telefono1 = telefono1;
            this.telefono2 = telefono2;
            this.correo = correo;
            this.entidad = entidad;
        }

        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [Required]
        [DisplayName("Telefono")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        [RegularExpression("[0-9]{10}",
         ErrorMessage = "Numero Invalido")]
        public string telefono1 { get; set; }

        [StringLength(15)]
        [DisplayName("Telefono opcional")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{10}",
         ErrorMessage = "Numero Invalido")]
        public string telefono2 { get; set; }

        [StringLength(200)]
        [DisplayName("Correo")]
        [DataType(DataType.EmailAddress)]
        public string correo { get; set; }

        [Required]
        public int entidad { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Contacto contacto &&
                   id == contacto.id;
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