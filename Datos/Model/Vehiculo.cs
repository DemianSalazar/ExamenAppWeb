using System;
using System.Collections.Generic;

#nullable disable

namespace Datos.Model
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            TipoVehiculos = new HashSet<TipoVehiculo>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<TipoVehiculo> TipoVehiculos { get; set; }
    }
}
