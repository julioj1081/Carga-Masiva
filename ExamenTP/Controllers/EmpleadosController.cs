using System.IO;
using System.Web.Mvc;
using System.Text;

using System.Xml;
using System.Xml.Linq;
using SpreadsheetLight;
using System;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace ExamenTP.Controllers
{
    public class EmpleadosController : Controller
    {

        // GET: Empleados
        [HttpGet]
        public ActionResult GetAllEmpleado()
        {
            ML.Empleado usuarios = new ML.Empleado();

            ML.Result result = new ML.Result();
            result = BL.Empleado.UsuariosGetAll();
            usuarios.Empleados = result.Objects;
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult AddEmpleado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ML.Empleado Usuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Empleado.AddUsuario(Usuario);
            ViewBag.Message = "Empleado registrado";
            return PartialView("modal");
        }


        [HttpGet]
        public ActionResult GetEmpleado(string RFC)
        {
            ML.Result result = new ML.Result();
            result = BL.Empleado.GetByEmpleado(RFC);

            if (result.Correct)
            {
                ML.Empleado usuario = new ML.Empleado();
                usuario = (ML.Empleado)result.Object;
                return View(usuario);
            }
            return null;
        }

        [HttpPost]
        public ActionResult Update(ML.Empleado Empleado)
        {
            ML.Result result = new ML.Result();
            result = BL.Empleado.UpdateEmpleado(Empleado);
            ViewBag.Message = "Empleado Modificado";
            return PartialView("modal");
        }

        [HttpGet]
        public ActionResult Delete(string RFC)
        {
            ML.Result result = new ML.Result();
            result = BL.Empleado.DeleteEmpleado(RFC);
            ViewBag.Message = "Empleado eliminado";
            return PartialView("modal");
        }

        [HttpGet]
        public FileResult TxtReporte(string RFC)
        {
            ML.Result result = new ML.Result();
            result = BL.Empleado.GetByEmpleado(RFC);
            ML.Empleado empleado = new ML.Empleado();
            empleado = (ML.Empleado)result.Object;

            string text = empleado.RFC + "|" + empleado.NumeroEmpleado + "|" + empleado.Nombre + "|" + empleado.ApellidoPaterno + "|" + empleado.ApellidoMaterno + "|" + empleado.FechaControl + "|" + empleado.Salario;
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
            return File(stream, "text/plain", "Reporte.txt");
        }

        [HttpGet]
        public FileStreamResult XMLReporte(string RFC)
        {
            ML.Result result = new ML.Result();
            result = BL.Empleado.GetByEmpleado(RFC);
            ML.Empleado empleado = new ML.Empleado();
            empleado = (ML.Empleado)result.Object;

            //proceso que genera el xml

            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(ms, xws))
            {
                XDocument doc = new XDocument(
                 new XElement("Empleado",
                  new XElement("RFC", empleado.RFC),
                  new XElement("NumeroEmpleado", empleado.NumeroEmpleado),
                  new XElement("Nombre", empleado.Nombre),
                  new XElement("ApellidoPaterno", empleado.ApellidoPaterno),
                  new XElement("ApellidoMaterno", empleado.ApellidoMaterno),
                  new XElement("FechaControl", empleado.FechaControl),
                  new XElement("Salario", empleado.Salario)
                 )
                );
                doc.WriteTo(xw);
            }
            ms.Position = 0;
            return File(ms, "text/xml", "ReporteXML.xml");
        }

        [HttpGet]
        public FileResult ExcReporte(string RFC)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Reporte_Invididual.xlsx";
            ML.Result result = new ML.Result();
            result = BL.Empleado.GetByEmpleado(RFC);
            ML.Empleado empleado = new ML.Empleado();
            empleado = (ML.Empleado)result.Object;

            /*instalar spreadsheetlight 3.4.9*/
            SLDocument doc = new SLDocument();
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("RFC", typeof(string));
            dt.Columns.Add("NumUsuario", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("ApellidoPaterno", typeof(string));
            dt.Columns.Add("ApellidoMaterno", typeof(string));
            dt.Columns.Add("FechaControl", typeof(string));
            dt.Columns.Add("Salario", typeof(float));

            dt.Rows.Add(
                empleado.RFC,
                empleado.NumeroEmpleado,
                empleado.Nombre,
                empleado.ApellidoPaterno,
                empleado.ApellidoMaterno,
                empleado.FechaControl,
                empleado.Salario);

            doc.ImportDataTable(1, 1, dt, true);
            doc.SaveAs(path);
            return File(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Invididual.xlsx");
        }

        [HttpGet]
        public FileResult ExcelReporte()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Reporte_General.xlsx";

            ML.Result result = new ML.Result();
            result = BL.Empleado.UsuariosGetAll();
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empleados = result.Objects;

            /*instalar spreadsheetlight 3.4.9*/
            SLDocument doc = new SLDocument();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("RFC", typeof(string));
            dt.Columns.Add("NumUsuario", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("ApellidoPaterno", typeof(string));
            dt.Columns.Add("ApellidoMaterno", typeof(string));
            dt.Columns.Add("FechaControl", typeof(string));
            dt.Columns.Add("Salario", typeof(float));
   
            foreach (ML.Empleado item in empleado.Empleados)
            {
                dt.Rows.Add(
                    item.RFC,
                    item.NumeroEmpleado,
                    item.Nombre,
                    item.ApellidoPaterno,
                    item.ApellidoMaterno,
                    item.FechaControl,
                    item.Salario);

                doc.ImportDataTable(1, 1, dt, true);
            }
            doc.SaveAs(path);
            return File(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_General.xlsx");
        }
    
        [HttpGet]
        public FileStreamResult JsonReporte()
        {

            ML.Result result = new ML.Result();
            result = BL.Empleado.UsuariosGetAll();
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empleados = result.Objects;
            string text = JsonConvert.SerializeObject(empleado.Empleados);
            //string text = empleado.RFC + "|" + empleado.NumeroEmpleado + "|" + empleado.Nombre + "|" + empleado.ApellidoPaterno + "|" + empleado.ApellidoMaterno + "|" + empleado.FechaControl + "|" + empleado.Salario;
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
            return File(stream, "text/plain", "Reporte.txt");
        }

        
    }
}
