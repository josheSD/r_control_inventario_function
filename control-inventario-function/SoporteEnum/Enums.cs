using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_function.SoporteEnum
{
    public  class Enums
    {
        public enum EstadoProyecto
        {
            [Description("Vigente")]
            Vigente = 0,
            [Description("Concluido")]
            Concluido = 1,
            [Description("Inactivo")]
            Inactivo = 2,
        }

        public enum EstadoUsuario
        {
            [Description("Inactivo")]
            Inactivo = 0,
            [Description("Activo")]
            Activo = 1,
        }
        public enum EstadoArticulo
        {
            [Description("Inactivo")]
            Inactivo = 0,
            [Description("Activo")]
            Activo = 1,
        }
        public enum EstadoAlmacen
        {
            [Description("Inactivo")]
            Inactivo = 0,
            [Description("Activo")]
            Activo = 1,
        }

        public enum EstadoArticuloAlmacen
        {
            [Description("Inactivo")]
            Inactivo = 0,
            [Description("Activo")]
            Activo = 1,
            [Description("Antigua")]
            Antigua = 2,
        }

        public enum EstadoProyectoAlmacen
        {
            [Description("Inactivo")]
            Inactivo = 0,
            [Description("Activo")]
            Activo = 1,
            [Description("Antigua")]
            Antigua = 2,
        }


    }
}
