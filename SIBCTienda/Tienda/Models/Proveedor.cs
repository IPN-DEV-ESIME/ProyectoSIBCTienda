using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class Proveedor
    {
        //Atributos
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(13)]
        public string Rfc { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [DisplayName("Pagina Web")]
        public string PaginaWeb { get; set; }

        public Contacto contacto { get; set; }  
        public Direccion direccion { get; set; }

        //Constructores
    
        public Proveedor()
        {
        }

        public Proveedor(int id, string nombre, string rfc, string paginaWeb, Contacto contacto, Direccion direccion)
        {
            Id = id;
            Nombre = nombre;
            Rfc = rfc;
            PaginaWeb = paginaWeb;
            this.contacto = contacto;
            this.direccion = direccion;
        }

        //Metodos

        public override bool Equals(object obj)
        {
            return obj is Proveedor proveedores &&
                   Id == proveedores.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}