using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EjemploWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public List<Usuarios> ObtenerUsuarios()
        {
            contextoDatosDataContext contexto = new contextoDatosDataContext();
            //Consulta en la tabla de Usuarios
            return (from r in contexto.Usuarios select r).ToList();
        }

        public bool VerificarAcceso(string user, string pass)
        {
            contextoDatosDataContext contexto = new contextoDatosDataContext();
            List<Usuarios> datos = (from r in contexto.Usuarios where r.nombreUsuario.Equals(user) && r.password.Equals(pass) select r).ToList();
            if(datos.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InsertarUsuario(string user, string pass)
        {
            contextoDatosDataContext contexto = new contextoDatosDataContext();
            Usuarios add_user = new Usuarios();
            add_user.nombreUsuario = user;
            add_user.password = pass;
            //Insertar usuario
            contexto.Usuarios.InsertOnSubmit(add_user);
            //Confirmar cambios
            contexto.SubmitChanges();
        }

        public void EliminarUsuario(string user)
        {
            contextoDatosDataContext contexto = new contextoDatosDataContext();
            List<Usuarios> datos = (from r in contexto.Usuarios where r.nombreUsuario.Equals(user) select r).ToList();
            if (datos.Count > 0)
            {
                //Eliminar usuario
                contexto.Usuarios.DeleteOnSubmit(datos.FirstOrDefault());
                //Confirmar cambios
                contexto.SubmitChanges();
            }
        }

        public bool DesactivarUsuario(string user)
        {
            contextoDatosDataContext contexto = new contextoDatosDataContext();
            List<Usuarios> datos = (from r in contexto.Usuarios where r.nombreUsuario.Equals(user) select r).ToList();
            if (datos.Count > 0)
            {
                datos.FirstOrDefault().activo = false;
                //Confirmar cambios
                contexto.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

