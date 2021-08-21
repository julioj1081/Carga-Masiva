using System;
using System.Xml;
using System.Text;
namespace ExamenTP.Controllers
{
    public class ReporteXML
    {
        public void GenerateXML(object Empleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado = (ML.Empleado)Empleado;
            var file_path = @"C:\Users\ALIEN10\Documents\FernandezGonzalezJulioAlberto\ExamenTP\ReporteXML.xml";
            XmlTextWriter writer;
            writer = new XmlTextWriter(file_path, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Empleado");
            writer.WriteElementString("RFC", empleado.RFC);
            writer.WriteElementString("NumeroEmpleado", Convert.ToString(empleado.NumeroEmpleado));
            writer.WriteElementString("Nombre", Convert.ToString(empleado.Nombre));
            writer.WriteElementString("ApellidoPaterno", Convert.ToString(empleado.ApellidoPaterno));
            writer.WriteElementString("ApellidoMaterno", Convert.ToString(empleado.ApellidoMaterno));
            writer.WriteElementString("FechaControl", Convert.ToString(empleado.FechaControl));
            writer.WriteElementString("Salario", Convert.ToString(empleado.Salario));
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }
}