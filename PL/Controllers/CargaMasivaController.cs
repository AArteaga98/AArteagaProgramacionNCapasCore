using Microsoft.AspNetCore.Mvc;
using ML;
using System.IO;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        // EXCEL

        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //F.Excel

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result= new ML.Result();
            return View(result);
        }

        [HttpPost]
        public ActionResult CargaTXT()
        {
            IFormFile fileTXT = Request.Form.Files["archivoTXT"];


            if (fileTXT != null)
            {
                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());
                string line;
                line = Textfile.ReadLine();

                ML.Result resultm = new ML.Result();
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
                            "No se inserto el Nombre:  " + usuario.Nombre  +
                            " \nNo se inserto el ApellidoP:   " + usuario.ApellidoPaterno +
                            " \nNo se inserto el ApellidoM:  " + usuario.ApellidoMaterno +
                            " \nNo se inserto la Fecha de Nacimiento:  " + usuario.FechaNacimiento +
                            " \nNo se inserto el Genero:  " + usuario.Genero+
                            " \nNo se inserto el UserName:  " + usuario.UserName+
                            " \nNo se inserto el Email:  " + usuario.Email+
                            " \nNo se inserto el Password:  " + usuario.Password+
                            " \nNo se inserto el Telefono:  " + usuario.Telefono+
                            " \nNo se inserto el Celular:  " + usuario.Celular+
                            " \nNo se inserto el CURP:  " + usuario.UserName+
                            " \nNo se inserto el Rol:  " + usuario.CURP+
                            " \nNo se inserto el Imagen:  " + usuario.Imagen+
                            " \nNo se inserto la calle:  " + usuario.Direccion.Calle+
                            " \nNo se inserto el numero interior:  " + usuario.Direccion.NumeroInterior+
                            " \nNo se inserto el numero exterior:  " + usuario.Direccion.NumeroExterior+
                            " \nNo se inserto el IdColonia:  " + usuario.Direccion.Colonia.IdColonia
                            );
                    } //Se le asigna agrega la lista de errores
                }
             
                if (resultErrores.Objects != null)
                {
                    ViewBag.Message = "ERROR: " + resultErrores.Message;
                    
                }
                else
                {
                    ViewBag.Message = resultErrores.Message;


                }

                TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\Arturo Arteaga\CargaMasiva\Errores.txt");
                foreach (string error in resultErrores.Objects)
                {
                    tw.WriteLine(error); //Se le concatenan todos los errores
                }
                tw.Close();


            }
            return PartialView("Modal");
        }

        // excel

        [HttpPost]
        public ActionResult UsuarioCargaMasiva(ML.Usuario usuario)
        {

            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null)  //sesion nula o que no exista 
            {
                //validar el excel

                if (excelCargaMasiva.Length > 0)
                {
                    //que sea .xlsx
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //crear copia
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream);
                            }
                            //leer
                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            //convertExceltodatatable

                            ML.Result resultConvertExcel = BL.Usuario.ConvertirExceltoDataTable(connectionString);

                            if (resultConvertExcel.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultConvertExcel.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View("CargaMasiva", resultValidacion);
                            }
                            else
                            {
                                //error al leer el archivo
                                ViewBag.Message = "Ocurrio un error al leer el arhivo";
                                return View("Modal");
                            }
                        }
                    }

                }



                //verificar que no tenga errores 
                //le avisamos al usuario que el excel esta mal ML.ErrorExcel 
                //crea la sesion 
            }
            else
            {

                //add 
                //errores al agregar 

                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;
                ML.Result resultData = BL.Usuario.ConvertirExceltoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Usuario con nombre: " + usuarioItem.Nombre + " Error: " + resultAdd.Message);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"C:\Users\digis\Documents\Arturo Arteaga\AArteagaProgramacionNCapas\PL\wwwroot\Files\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Usuarios han sido registrados correctamente";

                    }

                }

            }
            return PartialView("Modal");

        }

    }
}