using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
namespace BL
{
    public class Empleado
    {
        public static ML.Result AddUsuario(ML.Empleado usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JFernandezTPEntities2 context = new DL.JFernandezTPEntities2())
                {
                    var query = context.AddUsuario(usuario.RFC, (int) usuario.NumeroEmpleado, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,usuario.FechaControl, (decimal)usuario.Salario);

                    //var query = context.AddUsuario(usuario.RFC, usuario.NumeroEmpleado, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.FechaControl, usuario.Salario);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }

        public static ML.Result UsuariosGetAll()
        {
            var provider = new CultureInfo("fr-FR");
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JFernandezTPEntities2 context = new DL.JFernandezTPEntities2())
                {
                    var query = context.GetAllEmpleados().ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach(var item in query){
                            ML.Empleado usuario = new ML.Empleado();
                            usuario.RFC = item.RFC;
                            usuario.NumeroEmpleado = (int)item.NumUsuario;
                            usuario.Nombre = item.Nombre;
                            usuario.ApellidoPaterno = item.ApellidoPaterno;
                            usuario.ApellidoMaterno = item.ApellidoMaterno;
                            usuario.FechaControl = DateTime.Parse(item.FechaControl);
                            usuario.Salario = decimal.Parse(item.Salario.ToString());
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = true;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }


        public static ML.Result GetByEmpleado(string RFC)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JFernandezTPEntities2 context = new DL.JFernandezTPEntities2())
                {
                    var resultado = (from e in context.GetAllEmpleados()
                                     where e.RFC == RFC
                                     select e).FirstOrDefault();
                    if (resultado != null)
                    {
                        ML.Empleado Usuario = new ML.Empleado();
                        Usuario.RFC = resultado.RFC;
                        Usuario.NumeroEmpleado = (int) resultado.NumUsuario;
                        Usuario.Nombre = resultado.Nombre;
                        Usuario.ApellidoPaterno = resultado.ApellidoPaterno;
                        Usuario.ApellidoMaterno = resultado.ApellidoMaterno;
                        Usuario.FechaControl = DateTime.Parse(resultado.FechaControl);
                        Usuario.Salario = (decimal)resultado.Salario;

                        result.Object = Usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }


        public static ML.Result UpdateEmpleado(ML.Empleado usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JFernandezTPEntities2 context = new DL.JFernandezTPEntities2())
                {
                    var query = context.UpdateUsuario(usuario.RFC, (int)usuario.NumeroEmpleado, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.FechaControl, (decimal)usuario.Salario);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }


        public static ML.Result DeleteEmpleado(string RFC)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JFernandezTPEntities2 context = new DL.JFernandezTPEntities2())
                {
                    var query = context.DeleteUsuario(RFC);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }
    }
}
