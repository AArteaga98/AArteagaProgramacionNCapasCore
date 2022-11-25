using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    var query = context.Empresas.FromSqlRaw("EmpresaGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;


                            result.Objects.Add(empresa);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                throw;
            }
            return result;
        }

        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    //int query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.FechaNacimiento, usuario.Genero, usuario.UserName, usuario.Email, usuario.Password, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    var empresas = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'  ");

                    if (empresas > 0)
                    {
                        result.Message = "Empresa Registrado con exito";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al agregar usuario" + result.Ex;
                throw;
            }
            return result;
        }

        public static ML.Result GetById(int? IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    var query = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = query.IdEmpresa;
                            empresa.Nombre = query.Nombre;
                            empresa.Telefono = query.Telefono;
                            empresa.Email = query.Email;
                            empresa.DireccionWeb = query.DireccionWeb;
                            empresa.Logo = query.Logo;


                        result.Object = empresa;
                        result.Correct = true;

                        
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {

                    var empresas = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa},'{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'  ");

                    if (empresas > 0)
                    {
                        result.Message = "Empresa Modificada con exito";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al modificar empresa" + result.Ex;
                throw;
            }
            return result;
        }
        public static ML.Result Delete(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpresaDelete {empresa.IdEmpresa}");

                    if (query > 0)
                    {
                        result.Message = "Empresa Eliminada con exito";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar empresa" + result.Ex;
                throw;
            }
            return result;
        }

    }
}
