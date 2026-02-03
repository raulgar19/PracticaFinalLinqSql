using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using PracticaFinalLinqSql.Models;
using System.Data;

namespace PracticaFinalLinqSql.Repositories
{
    #region VISTAS
    //CREATE VIEW V_DETALLES_ALUMNO AS
    //SELECT
    //    U.IDUSUARIO,
    //    U.NOMBRE AS NombreAlumno,
    //    U.APELLIDOS,
    //    U.EMAIL,
    //    U.IMAGEN,
    //    A.nombre AS NombreActividad,
    //    I.fecha_inscripcion,
    //    I.quiere_ser_capitan,
    //    C.NOMBRE AS NombreCurso
    //FROM
    //    USUARIOSTAJAMAR U
    //    INNER JOIN CURSOSTAJAMAR C
    //        ON U.IDCURSO = C.IDCURSO
    //    INNER JOIN INSCRIPCIONES I
    //        ON U.IDUSUARIO = I.id_usuario
    //    INNER JOIN EVENTO_ACTIVIDADES EA
    //        ON I.IdEventoActividad = EA.IdEventoActividad
    //    INNER JOIN ACTIVIDADES A
    //        ON EA.IdActividad = A.id_actividad;
    //GO
    #endregion
    public class RepositoryMarta
    {
        private DataTable tablaUsuario;
        private DataTable tablaVistaDetalles;

        public RepositoryMarta()
        {
            string connectionString = "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=SegundaPractica;User ID=SA;Trust Server Certificate=True";

            string sqlUsuarios = "SELECT * FROM USUARIOSTAJAMAR";
            SqlDataAdapter ad = new SqlDataAdapter(sqlUsuarios, connectionString);
            this.tablaUsuario = new DataTable();
            ad.Fill(this.tablaUsuario);

            string sqlVista = "SELECT * FROM V_DETALLES_ALUMNO";
            ad = new SqlDataAdapter(sqlVista, connectionString);
            this.tablaVistaDetalles = new DataTable();
            ad.Fill(this.tablaVistaDetalles);
        }

        public List<Usuario> GetUsuarios()
        {
            var consulta = from datos in this.tablaUsuario.AsEnumerable()
                           select datos;

            List<Usuario> usuarios = new List<Usuario>();

            foreach (var row in consulta)
            {
                Usuario usuario = new Usuario();
                usuario.IdUsuario = row.Field<int>("IdUsuario");
                usuario.Nombre = row.Field<string>("Nombre");
                usuario.Apellidos = row.Field<string>("Apellidos");
                usuario.Email = row.Field<string>("Email");
                usuario.Imagen = row.Field<string>("Imagen");
                usuario.IdCurso = row.Field<int>("IdCurso");
                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public DetallesAlumno GetDetails(int id)
        {
            var consulta = from datos in this.tablaVistaDetalles.AsEnumerable()
                           where datos.Field<int>("IDUSUARIO") == id
                           select datos;

            var row = consulta.FirstOrDefault();

            if (row != null)
            {
                DetallesAlumno detalles = new DetallesAlumno();

                detalles.IdUsuario = row.Field<int>("IDUSUARIO");
                detalles.Nombre = row.Field<string>("NombreAlumno");
                detalles.Apellidos = row.Field<string>("APELLIDOS");
                detalles.Email = row.Field<string>("EMAIL");
                detalles.Imagen = row.Field<string>("IMAGEN");
                detalles.NombreActividad = row.Field<string>("NombreActividad");
                detalles.FechaInscripcion = row.Field<DateTime>("fecha_inscripcion");
                detalles.QuiereSerCapitan = row.Field<bool>("quiere_ser_capitan");
                detalles.NombreCurso = row.Field<string>("NombreCurso");

                return detalles;
            }
            else
            {
                return null;
            }
        }
    }
}