using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class Categoria
    {
        //Atributos
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; }

        //Constructores
        public Categoria()
        {
        }

        public Categoria(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}