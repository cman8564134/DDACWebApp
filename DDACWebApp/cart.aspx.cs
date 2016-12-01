using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using QC = System.Data.SqlClient;  // System.Data.dll  
using DT = System.Data;
using System.Web.Script.Services;
using System.Web.Configuration;
using System.Configuration;
using DDACWebApp.Models;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

namespace DDACWebApp
{
    public partial class cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the Facebook code from the querystring
            if (!ListView1.Items.Any())
            { 
                if (Request.QueryString["code"] != "")
                {
                    var obj = GetFacebookUserData(Request.QueryString["code"]);

                    ListView1.DataSource = obj;
                    ListView1.DataBind();

                }
        }
        }
        private List<FacebookUser> GetFacebookUserData(string code)
        {
            try
            {
                // Exchange the code for an access token
                Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/cart.aspx&code=" + code);
                HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);
                at.Method = "POST";
                System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
                string token = str.ReadToEnd().ToString().Replace("access_token=", "");

                // Split the access token and expiration from the single string
                string[] combined = token.Split('&');
                string accessToken = combined[0];

                // Exchange the code for an extended access token
                Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
                HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

                StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
                string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

                // Split the access token and expiration from the single string
                string[] eatWords = eatToken.Split('&');
                string extendedAccessToken = eatWords[0];

                // Request the Facebook user information
                Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,gender,locale,link&access_token=" + accessToken);
                HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

                // Read the returned JSON object response
                StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
                string jsonResponse = string.Empty;
                jsonResponse = userInfo.ReadToEnd();

                // Deserialize and convert the JSON object to the Facebook.User object type
                JavaScriptSerializer sr = new JavaScriptSerializer();
                string jsondata = jsonResponse;
                FacebookUser converted = sr.Deserialize<FacebookUser>(jsondata);

                // Write the user data to a List
                List<FacebookUser> currentUser = new List<FacebookUser>();
                currentUser.Add(converted);

                // Return the current Facebook user
                return currentUser;
            }
            catch
            {
                return null;
            }

        }
        [WebMethod,ScriptMethod]
        public static string insert(string a)
        {
            try
            {
                using (QC.SqlConnection connection = new QC.SqlConnection(
    WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString))
                {
                    connection.Open();
                    cart.SelectRows(connection);

                    connection.Close();
                //      connection.Open();
                 //     cart.InsertRows(connection);
                  //  connection.Close();

                    
                }
                return "successfully inserted";
            }
            catch
            { return "failed to insert"; }
            
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
                parameter.Value = "ssaassSQL Server Express 2014";
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@ProductNumber", DT.SqlDbType.NVarChar, 25);
                parameter.Value = "saasSQLEXPRESS2014";
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