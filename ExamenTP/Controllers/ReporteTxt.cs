using System.IO;
namespace ExamenTP.Controllers
{
    public class ReporteTxt
    {
        public void GenerateTxt(object Empleado){
            string path = @"C:\Users\ALIEN10\Documents\FernandezGonzalezJulioAlberto\ExamenTP\ReporteTXT.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter outputFile = new StreamWriter(path))
                {
                    ML.Empleado empleado = new ML.Empleado();
                    empleado = (ML.Empleado)Empleado;
                    outputFile.WriteLine(empleado.RFC + "|" + empleado.NumeroEmpleado + "|" + empleado.Nombre + "|" + empleado.ApellidoPaterno + "|" + empleado.ApellidoMaterno + "|" + empleado.FechaControl + "|" + empleado.Salario);
                }
            }
            else
            {
                using (StreamWriter outputFile = new StreamWriter(path))
                {
                    ML.Empleado empleado = new ML.Empleado();
                    empleado = (ML.Empleado)Empleado;
                    outputFile.WriteLine(empleado.RFC + "|" + empleado.NumeroEmpleado + "|" + empleado.Nombre + "|" + empleado.ApellidoPaterno + "|" + empleado.ApellidoMaterno + "|" + empleado.FechaControl + "|" + empleado.Salario);
                }
            }
            ///
            
        }
    }
}