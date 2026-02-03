using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using PracticaFinalLinqSql.Models;
using System.Collections.Generic;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PracticaFinalLinqSql.Repositories
{
    #region PROCEDURES
    //CREATE PROCEDURE SP_PLANTILLA_UPSERT
    //(@HOSPITAL_COD INT, @SALA_COD INT, @EMPLEADO_NO INT, @APELLIDO NVARCHAR(50),
    //@FUNCION NVARCHAR(50), @T NVARCHAR(50), @SALARIO INT)
    //AS
    //DECLARE @TOTALNOMBRE INT
    //SET @TOTALNOMBRE = (SELECT COUNT(*) FROM PLANTILLA WHERE PLANTILLA.EMPLEADO_NO = @EMPLEADO_NO)

    //IF @TOTALNOMBRE = 0
    //    BEGIN
    //        INSERT INTO PLANTILLA VALUES(@HOSPITAL_COD, @SALA_COD, @EMPLEADO_NO, @APELLIDO, @FUNCION, @T, @SALARIO)
    //    END
    //    ELSE
    //    BEGIN
    //        UPDATE PLANTILLA SET HOSPITAL_COD = @HOSPITAL_COD,SALA_COD=@SALA_COD,
    //        EMPLEADO_NO=@EMPLEADO_NO,APELLIDO=@APELLIDO,FUNCION=@FUNCION,T=@T,SALARIO=@SALARIO
    //        WHERE EMPLEADO_NO = @EMPLEADO_NO
    //    END
    //GO
    #endregion

    public class RepositoryPlantilla
    {
        private DataTable tablaPlantilla;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryPlantilla()
        {
            string connectionString = "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from PLANTILLA";
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);

            this.tablaPlantilla = new DataTable();

            ad.Fill(this.tablaPlantilla);

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Plantilla> GetPlantilla()
        {
            var consulta = from datos in this.tablaPlantilla.AsEnumerable()
                           select datos;

            List<Plantilla> plantilla = new List<Plantilla>();

            foreach (var datos in consulta)
            {
                Plantilla p = new Plantilla();
                p.HospitalCod = datos.Field<int>("HOSPITAL_COD");
                p.SalaCod = datos.Field<int>("SALA_COD");
                p.EmpleadoNo = datos.Field<int>("EMPLEADO_NO");
                p.Apellido = datos.Field<string>("APELLIDO");
                p.Funcion = datos.Field<string>("FUNCION");
                p.Turno = datos.Field<string>("T");
                p.Salario = datos.Field<int>("SALARIO");

                plantilla.Add(p);
            }

            return plantilla;
        }

        public async Task Create(int hospitalCod, int salaCod, int empleadoNo, string apellido, string funcion, string turno, int salario)
        {
            string sql = "SP_PLANTILLA_UPSERT";

            this.com.Parameters.Clear();

            this.com.CommandText = sql;
            this.com.CommandType = CommandType.StoredProcedure;

            this.com.Parameters.AddWithValue("@HOSPITAL_COD", hospitalCod);
            this.com.Parameters.AddWithValue("@SALA_COD", salaCod);
            this.com.Parameters.AddWithValue("@EMPLEADO_NO", empleadoNo);
            this.com.Parameters.AddWithValue("@APELLIDO", apellido);
            this.com.Parameters.AddWithValue("@FUNCION", funcion);
            this.com.Parameters.AddWithValue("@T", turno);
            this.com.Parameters.AddWithValue("@SALARIO", salario);

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();

            this.com.Parameters.Clear();
        }

        public List<string> GetFunciones()
        {
            var consulta = (from datos in this.tablaPlantilla.AsEnumerable() 
                           select datos.Field<string>("FUNCION")).Distinct();

            return consulta.ToList();
        }

        public List<Plantilla> GetPlantillaFuncion(string funcion)
        {
            var consulta = from datos in this.tablaPlantilla.AsEnumerable()
                           where datos.Field<string>("FUNCION") == funcion
                           select datos;

            List<Plantilla> plantilla = new List<Plantilla>();

            foreach (var datos in consulta)
            {
                Plantilla p = new Plantilla();
                p.HospitalCod = datos.Field<int>("HOSPITAL_COD");
                p.SalaCod = datos.Field<int>("SALA_COD");
                p.EmpleadoNo = datos.Field<int>("EMPLEADO_NO");
                p.Apellido = datos.Field<string>("APELLIDO");
                p.Funcion = datos.Field<string>("FUNCION");
                p.Turno = datos.Field<string>("T");
                p.Salario = datos.Field<int>("SALARIO");

                plantilla.Add(p);
            }

            return plantilla;
        }

        public async Task Update(int hospitalCod, int salaCod, int empleadoNo, string apellido, string funcion, string turno, int salario)
        {
            string sql = "SP_PLANTILLA_UPSERT";

            this.com.Parameters.Clear();

            this.com.CommandText = sql;
            this.com.CommandType = CommandType.StoredProcedure;

            this.com.Parameters.AddWithValue("@HOSPITAL_COD", hospitalCod);
            this.com.Parameters.AddWithValue("@SALA_COD", salaCod);
            this.com.Parameters.AddWithValue("@EMPLEADO_NO", empleadoNo);
            this.com.Parameters.AddWithValue("@APELLIDO", apellido);
            this.com.Parameters.AddWithValue("@FUNCION", funcion);
            this.com.Parameters.AddWithValue("@T", turno);
            this.com.Parameters.AddWithValue("@SALARIO", salario);

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();

            this.com.Parameters.Clear();
        }

        public Plantilla GetPlantillaEmpleado(int idEmpleado)
        {
            var consulta = from datos in this.tablaPlantilla.AsEnumerable()
                           where datos.Field<int>("EMPLEADO_NO") == idEmpleado
                           select datos;

            var row = consulta.First();

            Plantilla plantilla = new Plantilla();

            plantilla.HospitalCod = row.Field<int>("HOSPITAL_COD");
            plantilla.SalaCod = row.Field<int>("SALA_COD");
            plantilla.EmpleadoNo = row.Field<int>("EMPLEADO_NO");
            plantilla.Apellido = row.Field<string>("APELLIDO");
            plantilla.Funcion = row.Field<string>("FUNCION");
            plantilla.Turno = row.Field<string>("T");
            plantilla.Salario = row.Field<int>("SALARIO");

            return plantilla;
        }

        public async Task Delete(int idEmpleado)
        {
            string sql = "DELETE FROM PLANTILLA WHERE EMPLEADO_NO = @EMPLEADO_NO";

            this.com.Parameters.AddWithValue("@EMPLEADO_NO", idEmpleado);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            
            await this.cn.OpenAsync();

            await this.com.ExecuteNonQueryAsync();

            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}