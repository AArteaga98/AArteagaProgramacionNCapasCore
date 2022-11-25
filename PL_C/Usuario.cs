using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Usuario
    {


        public static void ReadFile()
        {

            string file = @"C:\Users\digis\Documents\Arturo Arteaga\CargaMasiva\Usuarios.txt";
            if (File.Exists(file))
            {


                StreamReader Textfile = new StreamReader(file);
                string line;
                line = Textfile.ReadLine();
                ML.Result resultErrores = new ML.Result();
                resultErrores.Objects = new List<object>();
                while ((line = Textfile.ReadLine()) != null)
                {


                    string[] lines = line.Split('|');

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Nombre = lines[0];
                    usuario.ApellidoPaterno = lines[1];
                    usuario.ApellidoMaterno = lines[2];
                    usuario.FechaNacimiento = lines[3];
                    usuario.Genero = lines[4];
                    usuario.UserName = lines[5];
                    usuario.Email = lines[6];
                    usuario.Password = lines[7];
                    usuario.Telefono = lines[8];
                    usuario.Celular = lines[9];
                    usuario.CURP = lines[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = byte.Parse(lines[11]);

                    usuario.Imagen = null;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Calle = lines[12];
                    usuario.Direccion.NumeroInterior = lines[14];
                    usuario.Direccion.NumeroExterior = lines[14];
                    usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

                    ML.Result result = BL.Usuario.Add(usuario);

                    if (!result.Correct) //si el resultado es diferente a correcto
                    {
                        resultErrores.Objects.Add(
                             "No se inserto el Nombre " + usuario.Nombre +
                            "No se inserto el ApellidoP " + usuario.ApellidoPaterno +
                            "No se inserto el ApellidoM" + usuario.ApellidoMaterno +
                            "No se inserto la Fecha de Nacimiento" + usuario.FechaNacimiento +
                            "No se inserto el Genero" + usuario.Genero +
                            "No se inserto el UserName" + usuario.UserName +
                            "No se inserto el Email" + usuario.Email +
                            "No se inserto el Password" + usuario.Password +
                            "No se inserto el Telefono" + usuario.Telefono +
                            "No se inserto el Celular" + usuario.Celular +
                            "No se inserto el CURP" + usuario.UserName +
                            "No se inserto el Rol" + usuario.CURP +
                            "No se inserto el Imagen" + usuario.Imagen +
                            "No se inserto la calle" + usuario.Direccion.Calle +
                            "No se inserto el numero interior" + usuario.Direccion.NumeroInterior +
                            "No se inserto el numero exterior" + usuario.Direccion.NumeroExterior +
                            "No se inserto el IdColonia" + usuario.Direccion.Colonia.IdColonia);
                    } //Se le asigna agrega la lista de errores
                }

                if (resultErrores.Objects != null)
                {

                }

                TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\Arturo Arteaga\CargaMasiva\Errores.txt");
                foreach (string error in resultErrores.Objects)
                {
                    tw.WriteLine(error); //Se le concatenan todos los errores
                }
                tw.Close();

                
            }
           
        }

    }
}  
