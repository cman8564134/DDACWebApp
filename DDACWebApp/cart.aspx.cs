﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using QC = System.Data.SqlClient;  // System.Data.dll  
using DT = System.Data;            // System.Data.dll  
using System.Web.Script.Services;

namespace DDACWebApp
{
    public partial class cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod,ScriptMethod]
        public static bool insert(string a)
        {
            
                using (var connection = new QC.SqlConnection(
                    "Server=tcp:my-new-server-object1234.database.windows.net,1433;Initial Catalog=my-database;Persist Security Info=False;User ID=my-admin-account;Password=p@ssw0rd1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                     ))
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");
                    cart.SelectRows(connection);

                connection.Close();
                connection.Open();
                cart.InsertRows(connection);
                    Console.WriteLine("Press any key to finish...");
                    Console.ReadKey(true);
                }
                return true;
            

        }

        public static string testing()
        {
            return "something";
        }
        private static void SelectRows(QC.SqlConnection connection)
        {
            using (var command = new QC.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
     SELECT  
        TOP 5  
            COUNT(soh.SalesOrderID) AS [OrderCount],  
            c.CustomerID,  
            c.CompanyName  
        FROM  
                            SalesLT.Customer         AS c  
            LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh  
                ON c.CustomerID = soh.CustomerID  
        GROUP BY  
            c.CustomerID,  
            c.CompanyName  
        ORDER BY  
            [OrderCount] DESC,  
            c.CompanyName; ";

                QC.SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2));
                }

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
                parameter.Value = "sssssSQL Server Express 2014";
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@ProductNumber", DT.SqlDbType.NVarChar, 25);
                parameter.Value = "sssssSQLEXPRESS2014";
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