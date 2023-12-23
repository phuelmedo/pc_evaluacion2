using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegocioPce
{
    public class Equipo : IPersistente
    {
        public int EquipoId { get; set; }
        public string NombreEquipo { get; set; }
        public int CantidadJugadores { get; set; }
        public string NombreDT { get; set; }
        public string TipoEquipo { get; set; }
        public string CapitanEquipo { get; set; }
        public bool TieneSub21 { get; set; }
        public bool Create()
        {
            try
            {
                CommonPce.ModelEquipo.spEquipoSave(
                    this.NombreEquipo,
                    this.CantidadJugadores,
                    this.NombreDT,
                    this.TipoEquipo,
                    this.CapitanEquipo,
                    this.TieneSub21
                );

                CommonPce.ModelEquipo.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                CommonPce.ModelEquipo.spEquipoDeleteById(id);
                CommonPce.ModelEquipo.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Read(int id)
        {
            try
            {
                LibraryPce.vwPCE equipo = CommonPce.ModelEquipo.vwPCE.First(l => l.EquipoId == id);

                this.EquipoId = equipo.EquipoId;
                this.NombreEquipo = equipo.NombreEquipo;
                this.CantidadJugadores = equipo.CantidadJugadores;
                this.NombreDT = equipo.NombreDT;
                this.TipoEquipo = equipo.TipoEquipo;
                this.CapitanEquipo = equipo.CapitanEquipo;
                this.TieneSub21 = equipo.TieneSub21;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                CommonPce.ModelEquipo.spEquipoUpdateById(
                    this.EquipoId,
                    this.NombreEquipo,
                    this.CantidadJugadores,
                    this.NombreDT,
                    this.TipoEquipo,
                    this.CapitanEquipo,
                    this.TieneSub21
                );

                CommonPce.ModelEquipo.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
