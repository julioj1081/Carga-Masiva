using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Globalization;
using System.Reflection;
namespace ExamenTP.Controllers
{
    public class CargaMasivaController : Controller
    {
        [HttpGet]
        // GET: CargaMasiva
        public ActionResult CargaMasiva()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase archivoTXT)
        {
            ML.Result result = new ML.Result();
            string linea;
            string[] Datos;
            DateTimeFormatInfo dtfi = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
            try
            {
                using (StreamReader lector = new StreamReader(archivoTXT.InputStream))
                {
                    int i = 0;
                    while (lector.Peek() > -1)
                    {
                        linea = lector.ReadLine();
                        Datos = linea.Split('\t');
                        if (i >= 1)
                        {
                            ML.Empleado usuario = new ML.Empleado();
                            usuario.RFC = Datos[0];
                            usuario.NumeroEmpleado = int.Parse(Datos[1]);
                            usuario.Nombre = Datos[2];
                            usuario.ApellidoPaterno = Datos[3];
                            usuario.ApellidoMaterno = Datos[4];
                            usuario.FechaControl = DateTime.Parse(Datos[5].ToString(dtfi));
                            usuario.Salario = decimal.Parse(Datos[6]);
                            result = BL.Empleado.AddUsuario(usuario);
                        }
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.mensaje = "Se produjo un error : " + e.Message;
            }
            ViewBag.Message = "Se produjo la carga masiva en la base de datos";
            return PartialView("_ModalView");
        }
    }
}