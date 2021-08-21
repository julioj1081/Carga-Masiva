using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
namespace ML
{
    public class Empleado
    {
        public string RFC { get; set; }
        public int NumeroEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        //"{0:dd/MM/yyyy}"

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss zzz}", ApplyFormatInEditMode = true)]
        public DateTime FechaControl { get; set; }
        public decimal Salario { get; set; }

        public List<object> Empleados { get; set; }

    }
}
