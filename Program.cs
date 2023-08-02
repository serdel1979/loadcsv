
using System;
using System.Data;
using Npgsql;

namespace CsvToPostgres
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = "ruta_del_archivo.csv"; // Ruta del archivo CSV a leer
            string connectionString = "Host=localhost;Username=usuario;Password=contraseña;Database=nombre_de_la_base_de_datos";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    // Crea la tabla si no existe
                    command.CommandText = "CREATE TABLE IF NOT EXISTS mi_tabla (columna1 tipo1, columna2 tipo2, ...);";
                    command.ExecuteNonQuery();

                    // Lee el archivo CSV y guarda los datos en la tabla
                    string[] lines = System.IO.File.ReadAllLines(csvFilePath);

                    foreach (string line in lines)
                    {
                        string[] fields = line.Split(',');
                        string column1Value = fields[0];
                        string column2Value = fields[1];
                        // ...

                        command.CommandText = $"INSERT INTO mi_tabla (columna1, columna2, ...) VALUES ('{column1Value}', '{column2Value}', ...);";
                        command.ExecuteNonQuery();
                    }
                }
            }

            Console.WriteLine("Datos del archivo CSV guardados en la tabla de la base de datos.");
        }
    }
}

