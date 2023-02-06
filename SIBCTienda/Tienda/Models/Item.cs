using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class Item
    {
        //Atributos
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Pedido")]
        public int PedidoId { get; set; }

        [Required]
        [DisplayName("Producto")]
        public int ProductoId { get; set; }

        [DisplayName("Cantidad")]
        public int CantidadProductos { get; set; }

        public float SubTotal { get; set; }


        //Constructores
        public Item()
        {
            CantidadProductos = 0;
            SubTotal = 0.0f;
        }

        public Item(int id, int pedidoId, int productoId, int cantidadProductos, float subTotal)
        {
            Id = id;
            PedidoId = pedidoId;
            ProductoId = productoId;
            CantidadProductos = cantidadProductos;
            SubTotal = subTotal;
        }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   Id == item.Id;
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