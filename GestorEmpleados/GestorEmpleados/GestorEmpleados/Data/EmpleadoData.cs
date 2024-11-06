using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class EmpleadoData
    {

        private readonly string conexion;

        public EmpleadoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }
        /// <summary>
        /// Consulta lista de empleados
        /// </summary>
        /// <returns></returns>
        public async Task<List<Empleado>> GetEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_selecciona", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) {
                        lista.Add(new Empleado
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre= reader["Nombre"].ToString(),
                            ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
        /// <summary>
        /// Agrega un empleado
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public async Task<bool> AddEmpleado(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
               
                SqlCommand cmd = new SqlCommand("sp_agregar", con);
                cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", objeto.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", objeto.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@fechacontrato", objeto.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true: false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        /// <summary>
        /// Actualiza un empleado
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEmpleado(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_actualizar", con);
                cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", objeto.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", objeto.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@fechaontrato", objeto.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        /// <summary>
        /// Elimina un empleado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEmpleado(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_eliminar", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
