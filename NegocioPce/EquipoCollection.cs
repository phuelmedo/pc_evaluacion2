using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioPce
{
    public class EquipoCollection
    {
        public List<Equipo> ReadAll()
        {
            var equipos = CommonPce.ModelEquipo.vwPCE;
            return ObtenerLibros(equipos.ToList());
        }

        private List<Equipo> ObtenerLibros(List<LibraryPce.vwPCE> equiposPce)
        {
            List<Equipo> equipoList = new List<Equipo>();
            foreach (LibraryPce.vwPCE equipo in equiposPce)
            {
                Equipo e = new Equipo();
                e.EquipoId = equipo.EquipoId;
                e.NombreEquipo = equipo.NombreEquipo;
                e.CantidadJugadores = equipo.CantidadJugadores;
                e.NombreDT = equipo.NombreDT;
                e.TipoEquipo = equipo.TipoEquipo;
                e.CapitanEquipo = equipo.CapitanEquipo;
                e.TieneSub21 = equipo.TieneSub21;

                equipoList.Add(e);
            }
            return equipoList;
        }
    }
}
