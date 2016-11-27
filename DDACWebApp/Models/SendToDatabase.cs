using System;  // C# , ADO.NET  
using DT = System.Data;            // System.Data.dll  
using QC = System.Data.SqlClient;  // System.Data.dll  
using DDACWebApp.Models;

namespace ProofOfConcept_SQL_CSharp
{
    public class Program
    {
        static public void connectAndInsert(Order o)
        {
            using (var connection = new QC.SqlConnection(
                "Server=tcp:YOUR_SERVER_NAME_HERE.database.windows.net,1433;Database=AdventureWorksLT;User ID=YOUR_LOGIN_NAME_HERE;Password=YOUR_PASSWORD_HERE;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                ))
            {
                connection.Open();
                Console.WriteLine("Connected successfully.");

                Program.InsertRows(connection);

                Console.WriteLine("Press any key to finish...");
                Console.ReadKey(true);
            }
        }

        static public void InsertRows(QC.SqlConnection connection)
        {
            QC.SqlParameter parameter;

            using (var command = new QC.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
    INSERT INTO SalesLT.Product  
            (Name,  
            ProductNumber,  
            StandardCost,  
            ListPrice,  
            SellStartDate  
            )  
        OUTPUT  
            INSERTED.ProductID  
        VALUES  
            (@Name,  
            @ProductNumber,  
            @StandardCost,  
            @ListPrice,  
            CURRENT_TIMESTAMP  
            ); ";

                parameter = new QC.SqlParameter("@Name", DT.SqlDbType.NVarChar, 50);
                parameter.Value = "SQL Server Express 2014";
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@ProductNumber", DT.SqlDbType.NVarChar, 25);
                parameter.Value = "SQLEXPRESS2014";
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@StandardCost", DT.SqlDbType.Int);
                parameter.Value = 11;
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@ListPrice", DT.SqlDbType.Int);
                parameter.Value = 12;
                command.Parameters.Add(parameter);

                int productId = (int)command.ExecuteScalar();
                Console.WriteLine("The generated ProductID = {0}.", productId);
            }
        }
    }
}