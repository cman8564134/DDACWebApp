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
using System.Diagnostics;
using System.Collections;

namespace DDACWebApp
{
    public partial class cart : System.Web.UI.Page
    {
        static string firstName;
        static string LastName;
        static string gender;
        List<FacebookUser> obj;
        private static int retryCount { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            retryCount = 3;
            // Get the Facebook code from the querystring
            if (!ListView1.Items.Any())
            { 
                if (Request.QueryString["code"] != "")
                {
                     obj = GetFacebookUserData(Request.QueryString["code"]);

                    ListView1.DataSource = obj;
                    ListView1.DataBind();
                }
        }

            //cast obj to whatever datatype it is

            if(obj.Count!=0)
            {
                firstName=obj[0].first_name;
                LastName=obj[0].last_name;
                gender=obj[0].gender;
            }
            else
            {
                ListView1.DataSource = null;
                ListView1.DataBind();
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
        [WebMethod, ScriptMethod]
        public static string[] insert(string[] a)
        {
            int currentRetry = 0;
            ArrayList Array = new ArrayList();
            

            using (QC.SqlConnection connection = new QC.SqlConnection(
        WebConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString))
            {
                connection.Open();
                QC.SqlCommand command = connection.CreateCommand();
                QC.SqlTransaction tran = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = tran;
                foreach (string o in a)
            {
                for (;;)
                {
                    try
                    {
                            Order or = new Order();
                        string[] x = o.Split('|');
                        or.Name = Convert.ToString(x[0]);
                        or.price = Convert.ToInt32(x[1]);
                        or.quantity = Convert.ToInt32(x[2]);
                        or.tourDate = Convert.ToString(x[3]);
                        or.OrderDate = Convert.ToDateTime(DateTime.Now);
                            Array.Add(cart.InsertRows(command, or));
                            break;
                        
                    }
                    catch
                    {
                        currentRetry++;
                        if (currentRetry > retryCount)
                            {
                                try
                                {
                                    tran.Rollback();

                                    Debug.WriteLine("failed to insert to database");
                                    break;
                                    
                                }
                                catch
                                {
                                    Debug.WriteLine("failed to rollback database");
                                    
                                }
                        }
                    }
                }
            }
                tran.Commit();
            }
            return (String[])Array.ToArray(typeof(string));


        }

        
        public static string InsertRows(QC.SqlCommand command,Order o)
        {
            QC.SqlParameter parameter;

            Debug.WriteLine("inside the insertrow:" + o.Name);
            command.CommandType = DT.CommandType.Text;
            command.CommandText = @"  
    INSERT INTO Trips  
            (Name,  
            Quantity,  
            Price,  
            Date,  
            OrderDate,
            Passenger_First_Name,
            Passenger_Last_Name,
            Passenger_Gender  
            )  
        OUTPUT  
            INSERTED.Id  
        VALUES  
            (@Name,  
            @quantity,  
            @price,  
            @date,  
            CURRENT_TIMESTAMP,
            @firstname,
            @lastname,
            @gender  
            ); ";

                parameter = new QC.SqlParameter("@Name", DT.SqlDbType.VarChar, 150);
                parameter.Value = o.Name;
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@quantity", DT.SqlDbType.Int);
                parameter.Value = o.quantity;
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@price", DT.SqlDbType.Int);
                parameter.Value = o.price;
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@date", DT.SqlDbType.VarChar,50);
                parameter.Value = o.tourDate;
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@firstname", DT.SqlDbType.VarChar,150);
                parameter.Value = firstName;
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@lastname", DT.SqlDbType.VarChar,150);
                parameter.Value = LastName;
                command.Parameters.Add(parameter);

                parameter = new QC.SqlParameter("@gender", DT.SqlDbType.VarChar,50);
                parameter.Value = gender;
                command.Parameters.Add(parameter);
            string a= Convert.ToString(command.ExecuteScalar());
            
            command.Parameters.Clear();
            return a;
        }
    }

}