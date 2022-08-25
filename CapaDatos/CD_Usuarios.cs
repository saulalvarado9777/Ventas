using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List <Usuario> Listar()
        {
            List<Usuario> Lista = new List<Usuario>();
            String Error;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_GetUsuarios", OConexion);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();
                    using (SqlDataReader Dr = Cmd.ExecuteReader())
                    {
                        while (Dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(Dr["IdUsuario"]),
                                Nombre = Convert.ToString(Dr["Nombre"]),
                                Apellidos = Convert.ToString(Dr["Apellidos"]),
                                Correo = Convert.ToString(Dr["Correo"]),
                                //Clave = Convert.ToString(Dr["Clave"]),
                                //Restablecer = Convert.ToBoolean(Dr["Restablecer"]),
                                Activo = Convert.ToBoolean(Dr["Activo"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Lista = new List<Usuario>();
                Error = ex.Message;
                throw;
            }

            return Lista;
        }
        public int RegistrarUsuarios(Usuario obj, out string Mensaje) /*Mpetodo para agregar usuarios a la base de datos*/
        {
            int IdAutogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    IdAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdAutogenerado = 0;
                Mensaje = ex.Message;
            }
            return IdAutogenerado;
        }

        public bool EditarUsuarios(Usuario obj, out string Mensaje) /*Método para editar usuarios*/
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_UpdUsuarios", OConexion);
                    Cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    Cmd.Parameters.AddWithValue("Nombres", obj.Nombre);
                    Cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    Cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    Cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();
                    Cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(Cmd.Parameters["Resultado"].Value);
                    Mensaje = Cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }
            return Resultado;
        }

        public bool EliminarUsuarios(int Id, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand Cmd = new SqlCommand("sp_DelUsuarios", OConexion);
                    Cmd.Parameters.AddWithValue("IdUsuario", Id);
                    Cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();
                    Resultado = Cmd.ExecuteNonQuery() > 0 ? true : false;

                    Resultado = Convert.ToBoolean(Cmd.Parameters["Resultado"].Value);
                    Mensaje = Cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }
            return Resultado;
        }
    }
}
