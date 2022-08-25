using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios OUsuarios = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return OUsuarios.Listar();
        }
        public int RegistrarUsuarios(Usuario obj, out string Mensaje) /*Mpetodo para agregar usuarios a la base de datos*/
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre)) //si es nulo o vacio ó es un espacio vacio 
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos)) //si es nulo o vacio ó es un espacio vacio 
            {
                Mensaje = "Los apellidos del usuario no puede ser vacios";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo)) //si es nulo o vacio ó es un espacio vacio 
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                string Clave = CN_Recursos.GenerarClave();
                string Asunto = "Creación de cuenta";
                string Mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es : !clave!</p>";

                Mensaje_correo = Mensaje_correo.Replace("!clave!", Clave);
                bool Respuesta = CN_Recursos.EnviarCorreo(obj.Correo, Asunto, Mensaje_correo);

                if (Respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(Clave);
                    return OUsuarios.RegistrarUsuarios(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public bool EditarUsuarios(Usuario obj, out string Mensaje) /*Método para editar usuarios*/
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre)) //si es nulo o vacio ó es un espacio vacio 
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos)) //si es nulo o vacio ó es un espacio vacio 
            {
                Mensaje = "Los apellidos del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo)) //si es nulo o vacio ó es un espacio vacio 
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return OUsuarios.EditarUsuarios(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }
        public bool EliminarUsuarios(int id, out string Mensaje)
        {
            return OUsuarios.EliminarUsuarios(id, out Mensaje);
        }

    }
}
