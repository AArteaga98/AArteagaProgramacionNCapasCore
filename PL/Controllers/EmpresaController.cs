using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empresa.GetAll();
            ML.Empresa empresa = new ML.Empresa();
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }

            return View(empresa);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {

            ML.Empresa empresa = new ML.Empresa();

            if (IdEmpresa == null)
            {

                return View(empresa);

            }
            else
            {
                //GetById
                ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);

                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;

                    return View(empresa);

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la Empresa seleccionado";
                }
                return View(empresa);
            }

            
        }



        [HttpPost]
        //ADD
        public ActionResult Form(ML.Empresa empresa)
        {
            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }

            ML.Result result = new ML.Result();


            if (empresa.IdEmpresa == 0)
            {

                result = BL.Empresa.Add(empresa);


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
                result = BL.Empresa.Update(empresa);
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


        // delete
        public ActionResult Delete(ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.Delete(empresa);
            if (result.Correct)
            {
                ViewBag.Message = result.Message;
            }
            else
            {
                ViewBag.Message = "ERROR: " + result.Message;
            }
            return PartialView("Modal");
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
