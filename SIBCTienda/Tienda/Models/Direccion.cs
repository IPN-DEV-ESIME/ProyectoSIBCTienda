using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tienda.Models
{
    public class Direccion
    {
        public Direccion() { }

        public Direccion(int entidad, string entidadFederativa, string codigoPostal, string colonia, string calle, string noInterior, string noExterior, string descripcion)
        {
            this.entidad = entidad;
            this.entidadFederativa = entidadFederativa;
            this.codigoPostal = codigoPostal;
            this.colonia = colonia;
            this.calle = calle;
            this.noInterior = noInterior;
            this.noExterior = noExterior;
            this.descripcion = descripcion;
        }

        public Direccion(int id, int entidad, string entidadFederativa, string codigoPostal, string colonia, string calle, string noInterior, string noExterior, string descripcion)
        {
            this.id = id;
            this.entidad = entidad;
            this.entidadFederativa = entidadFederativa;
            this.codigoPostal = codigoPostal;
            this.colonia = colonia;
            this.calle = calle;
            this.noInterior = noInterior;
            this.noExterior = noExterior;
            this.descripcion = descripcion;
        }

        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [Required]
        public int entidad { get; set; }

        [Required]
        [DisplayName("Entidad Federativa")]
        [StringLength(50)]
        public string entidadFederativa { get; set; }

        [Required]
        [DisplayName("Codigo Postal")]
        [StringLength(6)]
        public string codigoPostal { get; set; }

        [Required]
        [DisplayName("Colonia")]
        [StringLength(100)]
        public string colonia { get; set; }

        [Required]
        [DisplayName("Calle")]
        [StringLength(100)]
        public string calle { get; set; }

        [Required]
        [DisplayName("Numero Interior")]
        [StringLength(30)]
        public string noInterior { get; set; }

        [Required]
        [DisplayName("Numero Exterior")]
        [StringLength(30)]
        public string noExterior { get; set; }

        [DisplayName("Descripcion")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string descripcion { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Direccion direccion &&
                   id == direccion.id &&
                   entidad == direccion.entidad;
        }

        public override int GetHashCode()
        {
            int hashCode = -1145946510;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + entidad.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Direccion left, Direccion right)
        {
            return EqualityComparer<Direccion>.Default.Equals(left, right);
        }

        public static bool operator !=(Direccion left, Direccion right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}