using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class Pedido
    {
        //Atributos
        [Key]
        public int Id { get; set; }

        public bool Estatus { get; set; }

        [Required]
        [DisplayName("Usuario")]
        public int UsuarioId { get; set; }

        [DisplayName("Fecha del pedido")]
        public DateTime FechaPedido { get; set; }

        [DisplayName("Cantidad")]
        public int CantidadProductos { get; set; }

        [Range(0, 1, ErrorMessage = "% Descuento invalido (%10 -> 0.15)")]
        public float Descuento { get; set; }

        [Range(0.0, 1, ErrorMessage = "% IVA invalido")]
        public float Iva { get; set; }

        public float SubTotal { get; set; }

        public float Total { get; set; }

        //Constructores
        public Pedido()
        {
            Estatus = false;
            CantidadProductos = 0;
            Descuento = 0;
            Iva = 0.16f;
        }

        public Pedido(int id, bool estatus, DateTime fechaPedido, int cantidadProductos, float descuento, float iva, float subTotal, float total)
        {
            Id = id;
            Estatus = estatus;
            FechaPedido = fechaPedido;
            CantidadProductos = cantidadProductos;
            Descuento = descuento;
            Iva = iva;
            SubTotal = subTotal;
            Total = total;
        }

        public override bool Equals(object obj)
        {
            return obj is Pedido pedido &&
                   Id == pedido.Id;
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