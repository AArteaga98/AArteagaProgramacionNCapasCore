using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();


            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    //var responseTask = client.GetAsync("Usuario/GetAll"); 
                    var responseTask = client.GetAsync("Usuario/GetAll");
                    //result = bl.alumno.GetAll();

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            if (result.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los usuarios";
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            //usuario.Rol = new ML.Rol();

            ML.Result resultRol = BL.Rol.GetAll();
            result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
               
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles = resultRol.Objects;


                return View(usuario);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los usuarios";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {


            ML.Usuario usuario = new ML.Usuario();

            usuario.Rol = new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPaises = BL.Pais.GetAll();


            if (IdUsuario == null)
            {

                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                return View(usuario);

            }
            else
            {
                //GetById
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);

                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                    ML.Result resultEstados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    ML.Result resultColonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.IdColonia);

                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonias.Objects;


                    return View(usuario);

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el Usuario seleccionado";
                }
                return View(usuario);
            }
        }

        [HttpPost]
        //ADD
        public ActionResult Form(ML.Usuario usuario)
        {
           

            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }


            string urlAPI = _configuration["UrlAPI"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                //HTTP POST
                var postTask = client.PostAsJsonAsync("Usuario/Add", usuario);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }


            //Validacion

            if (ModelState.IsValid)
            {
                ML.Result result = new ML.Result();

                if (usuario.IdUsuario == 0)
                {

                    result = BL.Usuario.Add(usuario);


                    if (result.Correct)
                    {
                        ViewBag.Message = result.Message;
                    }
                    else
                    {
                        ViewBag.Message = "Error:  " + result.Message;
                    }

                }
                else
                {
                    //ACTUALIZAR POR SERVICIOS

                    //string urlAPI = _configuration["UrlAPI"];
                    //using (var client = new HttpClient())
                    //{
                    //    client.BaseAddress = new Uri(urlAPI);

                    //    //HTTP POST
                    //    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/update", usuario.IdUsuario, usuario);
                    //    postTask.Wait();

                    //    var result = postTask.Result;
                    //    if (result.IsSuccessStatusCode)
                    //    {
                    //        return RedirectToAction("GetAll");
                    //    }

                    result = BL.Usuario.Update(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Message = result.Message;
                    }
                    else
                    {
                        ViewBag.Message = "ERROR: " + result.Message;
                    }
                }

                return PartialView("Modal");





            }

            else
            {


                usuario.Rol = new ML.Rol();

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPaises = BL.Pais.GetAll();

                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                return View(usuario);

            }

           
        }

        // delete
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result resultUsuario = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;

            string urlAPI = _configuration["UrlAPI"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                //HTTP POST
                var postTask = client.DeleteAsync("Usuario/Delete/" + IdUsuario);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El usuario ha sido eliminado";
                }
                else
                {
                    ViewBag.Message = "El usuario no pudo ser eliminado" + resultUsuario.Message;
                }
            }

            return PartialView("Modal");


            ////ML.Result result = BL.Usuario.Delete(IdUsuario);
            //if (IdUsuario >= 1)
            //{
            //    ML.Result result = BL.Usuario.Delete(IdUsuario);
            //    ViewBag.Message = result.Message;
            //}
            //else
            //{
            //    ViewBag.Message = "ERROR: ";
            //}
            //return PartialView("Modal");
        }


        //JSON
        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);


            return Json(result.Objects);
        }

        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }

        public JsonResult CambiarStatus(int IdUsuario, bool Status)
        {
            var result = BL.Usuario.ChangeStatus(IdUsuario, Status);

            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }


    }
}
