using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class Producto
    {
        //Atributos
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }

        [Required]
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set;}

        [Required]
        [MaxLength(12)]
        public string Sku { get; set;}

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public float Precio { get; set; }

        public DateTime FechaRegistro { get; set; }

        //Constructores

        public Producto(int id, string nombre, int categoriaId, int proveedorId, string descripcion, string sku, int cantidad, float precio, DateTime fechaRegistro)
        {
            Id = id;
            Nombre = nombre;
            CategoriaId = categoriaId;
            ProveedorId = proveedorId;
            Descripcion = descripcion;
            Sku = sku;
            Cantidad = cantidad;
            Precio = precio;
            FechaRegistro = fechaRegistro;
        }

        public Producto()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Producto producto &&
                   Id == producto.Id;
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