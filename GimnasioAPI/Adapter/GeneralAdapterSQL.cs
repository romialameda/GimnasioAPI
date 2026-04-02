using Microsoft.Data.SqlClient;
using System.Data;

namespace GimnasioAPI.Adapter
{
    public class GeneralAdapterSQL
    {
        // 1. Configuramos la cadena de conexión a tu base de datos SQL.
        private string CadenaConexion = "Data Source=.\\SQLEXPRESS;Initial Catalog=GimnasioDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";

        /// <summary>
        /// Este método ejecuta una Vista en SQL y devuelve una tabla con los resultados.
        /// </summary>
        public DataTable EjecutarVista(string vista)
        {
            // Creamos la conexión fuera del try-catch para poder cerrarla en el finally
            using SqlConnection conexionBase = new SqlConnection(CadenaConexion);
            DataTable respuesta = new();

            try
            {
                // Preparamos la consulta
                string consulta = "SELECT * FROM " + vista;
                using var comando = new SqlCommand(consulta, conexionBase);
                comando.CommandType = CommandType.Text;

                // El adaptador ejecuta el comando y llena la tabla de respuesta
                SqlDataAdapter Adaptador = new(comando);
                conexionBase.Open();
                Adaptador.Fill(respuesta);
            }
            catch (Exception ex)
            {
                // Si algo falla, registramos el error en la tabla para que el Controlador lo sepa
                respuesta.Columns.Add("RESULTADO");
                respuesta.Rows.Add("ERROR");

                // NOTA: Aquí iría el Logger.RegistrarERROR que menciona el apunte, 
                // pero por ahora lo simplificamos para que te compile sin problemas.
            }
            finally
            {
                // Pase lo que pase, limpiamos y cerramos la conexión a la base de datos [4].
                SqlConnection.ClearAllPools();
                conexionBase.Close();
            }

            return respuesta;
        }

        public DataTable EjecutarProcedimiento(string procedimiento, Dictionary<string, object> parametros)
        {
            // Creamos la conexión 
            using SqlConnection conexionBase = new SqlConnection(CadenaConexion);
            DataTable respuesta = new();

            try
            {
                // Preparamos el comando usando el nombre del procedimiento
                using var comando = new SqlCommand(procedimiento, conexionBase);

                // ¡Diferencia 1! Notificamos a la base de datos que esto es un Procedimiento Almacenado
                comando.CommandType = CommandType.StoredProcedure;

                // ¡Diferencia 2! Recorremos el diccionario para agregar los parámetros (ej: @nombre, @apellido)
                foreach (var item in parametros)
                {
                    // Si el valor viene nulo desde C#, le avisamos a SQL enviando DBNull
                    if (item.Value == null || item.Value.ToString()?.Trim() == "NULL")
                    {
                        comando.Parameters.AddWithValue(item.Key, DBNull.Value);
                    }
                    else
                    {
                        // Agregamos el parámetro con su valor correspondiente
                        // Nota: El apunte oficial crea una función extra llamada GetDBType [2, 4], 
                        // pero también aclara que usar AddWithValue directamente suele funcionar perfecto para empezar [5].
                        comando.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }

                // El adaptador ejecuta el comando y llena la tabla de respuesta
                SqlDataAdapter Adaptador = new(comando);
                conexionBase.Open();
                Adaptador.Fill(respuesta);
            }
            catch (Exception ex)
            {
                // Si algo falla, registramos el error en la tabla para que el Controlador lo sepa
                respuesta.Columns.Add("RESULTADO");
                respuesta.Rows.Add("ERROR");

                // Aquí iría el Logger.RegistrarERROR...
            }
            finally
            {
                // Pase lo que pase, limpiamos y cerramos la conexión a la base de datos
                SqlConnection.ClearAllPools();
                conexionBase.Close();
            }

            return respuesta;
        }
    }
}
    

