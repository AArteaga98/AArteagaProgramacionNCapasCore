using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario)

        
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {

                    //usuario.Rol.IdRol = (usuario.Rol.IdRol == null) ? 0 : usuario.Rol.IdRol; //operador ternario
                    //var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll  '{usuario.Nombre} ', '{usuario.ApellidoPaterno}', {usuario.Rol.IdRol}  ").ToList();
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}' , '{usuario.ApellidoPaterno}', {usuario.Rol.IdRol}").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                       
                        foreach (var row in query)
                        {
                            ML.Usuario usuarioobj = new ML.Usuario();
                            usuarioobj.IdUsuario = row.IdUsuario;
                            usuarioobj.Nombre = row.Nombre;
                            usuarioobj.ApellidoPaterno = row.ApellidoPaterno;
                            usuarioobj.ApellidoMaterno = row.ApellidoMaterno;
                            usuarioobj.FechaNacimiento = row.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                            usuarioobj.Genero = row.Genero;
                            usuarioobj.UserName = row.UserName;
                            usuarioobj.Email = row.Email;
                            usuarioobj.Password = row.Password;
                            usuarioobj.Telefono = row.Telefono;
                            usuarioobj.Celular = row.Celular;
                            usuarioobj.CURP = row.Curp;

                            usuarioobj.Rol = new ML.Rol();
                            usuarioobj.Rol.IdRol = (byte)row.IdRol;
                            usuarioobj.Rol.Nombre = row.RolNombre;
                            usuarioobj.Imagen = row.Imagen;
                            usuarioobj.Status = row.Status.Value;

                            usuarioobj.Direccion = new ML.Direccion();
                            usuarioobj.Direccion.Colonia = new ML.Colonia();
                            usuarioobj.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioobj.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                            usuarioobj.Direccion.IdDireccion = (byte)row.IdDirreccion;
                            usuarioobj.Direccion.Calle = row.Calle;
                            usuarioobj.Direccion.NumeroInterior = row.NumeroInterior;
                            usuarioobj.Direccion.NumeroExterior = row.NumeroExterior;
                            usuarioobj.Direccion.Colonia.IdColonia = (byte)row.IdColonia;
                            usuarioobj.Direccion.Colonia.Nombre = row.ColoniaNombre;
                            usuarioobj.Direccion.Colonia.CP = row.CP;
                            usuarioobj.Direccion.Colonia.Municipio.IdMunicipio = (byte)row.IdMunicipio;
                            usuarioobj.Direccion.Colonia.Municipio.Nombre = row.MunicipioNombre;
                            usuarioobj.Direccion.Colonia.Municipio.Estado.IdEstado = (byte)row.IdEstado;
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Nombre = row.EstadoNombre;
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais.IdPais = (byte)row.IdPais;
                            usuarioobj.Direccion.Colonia.Municipio.Estado.Pais.Nombre = row.PaisNombre;


                            result.Objects.Add(usuarioobj);

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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    //int query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.FechaNacimiento, usuario.Genero, usuario.UserName, usuario.Email, usuario.Password, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaNacimiento}', '{usuario.Genero}', '{usuario.UserName}','{usuario.Email}','{usuario.Password}','{usuario.Telefono}','{usuario.Celular}','{usuario.CURP}',{usuario.Rol.IdRol},'{usuario.Imagen}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}  ");

                    if (usuarios > 0)
                    {
                        result.Message = "Usuario Registrado con exito";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al agregar usuario" + result.Ex;
               
            }
            return result;
        }

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.FechaNacimiento = query.FechaNacimiento.Value.ToString("dd/MM/yyyy");
                        usuario.Genero = query.Genero;
                        usuario.UserName = query.UserName;
                        usuario.Email =     query.Email;
                        usuario.Password = query.Password;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = (byte)query.IdRol;
                        usuario.Rol.Nombre = query.RolNombre;
                        usuario.Imagen = query.Imagen;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                        usuario.Direccion.IdDireccion = (byte)query.IdDirreccion;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.Colonia.IdColonia = (byte)query.IdColonia;
                        usuario.Direccion.Colonia.Nombre = query.ColoniaNombre;
                        usuario.Direccion.Colonia.CP = query.CP;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = (byte)query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.MunicipioNombre;
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = (byte)query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.EstadoNombre;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = (byte)query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.PaisNombre;



                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al obtener registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
                throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {

                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaNacimiento}', '{usuario.Genero}', '{usuario.UserName}','{usuario.Email}','{usuario.Password}','{usuario.Telefono}','{usuario.Celular}','{usuario.CURP}',{usuario.Rol.IdRol},'{usuario.Imagen}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}  ");

                    if (usuarios > 0)
                    {
                        result.Message = "Usuario Modificado con exito";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al modificar usuario" + result.Ex;
                throw;
            }
            return result;
        }

        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete { IdUsuario}");

                    if (query > 0)
                    {
                        result.Message = "Usuario Eliminado con exito";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar usuario" + result.Ex;
                throw;
            }
            return result;
        }

        public static ML.Result ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AarteagaProgramacionNcapasContext context = new DL.AarteagaProgramacionNcapasContext())
                {
                    //int query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.FechaNacimiento, usuario.Genero, usuario.UserName, usuario.Email, usuario.Password, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus {IdUsuario},{Status} ");

                    if (usuarios > 0)
                    {
                        result.Message = "Status modificado con exito";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al modificar status" + result.Ex;

            }
            return result;
        }

        //EXCEL{


        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.FechaNacimiento = row[3].ToString();
                                usuario.Genero = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                usuario.Email = row[6].ToString();
                                usuario.Password = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = byte.Parse(row[11].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Colonia = new ML.Colonia();

                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();
                                usuario.Direccion.Colonia.IdColonia = byte.Parse(row[15].ToString());

                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                           
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;

            }

            return result;

        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                  
                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : usuario.Nombre; //operador ternario

                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paterno ";
                    }

                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Materno ";
                    }

                    if(usuario.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar la Fecha de Nacimiento";
                    }

                    if (usuario.Genero == "")
                    {
                        error.Mensaje += "Ingresar el Genero";
                    }

                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "Ingresar el UserName";
                    }

                    if (usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el Email";
                    }

                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar el Password";
                    }

                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el Telefono";
                    }

                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresar el Celular";
                    }

                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresar el CURP";
                    }

                    if (usuario.Rol.IdRol.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el Rol ";
                    }

                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "Ingresar la Calle";
                    }

                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "Ingresar el Numero Interior";
                    }

                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "Ingresar el Numero Exterior";
                    }

                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el Id de Colonia";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;

            }

            return result;
        }



    }
}
