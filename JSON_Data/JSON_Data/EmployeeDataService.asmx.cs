using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using JSON_Data.myClass;
using System.Data;

namespace JSON_Data
{
    /// <summary>
    /// Summary description for EmployeeDataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmployeeDataService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat=ResponseFormat.Json,UseHttpGet =true)]
        public void getAllUsers()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            UsersData[] getUs = null;
            
            try
            {
                using (SqlConnection connection = new SqlConnection(DBconnect.ConnectionString))
                {
                    connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select * from LoginUser", connection);
                    sda.SelectCommand.CommandType = System.Data.CommandType.Text;
                    DataTable datatable = new DataTable();
                    sda.Fill(datatable);
                    getUs = new UsersData[datatable.Rows.Count];
                    int count = 0;
                    for(int i=0;i< datatable.Rows.Count; i++)
                    {
                        getUs[count] = new UsersData();
                        getUs[count].email = Convert.ToString(datatable.Rows[i]["email"]);
                        getUs[count].password = Convert.ToString(datatable.Rows[i]["password"]);
                        count++;
                    }
                    datatable.Clear();
                    connection.Close();
                }
            }
            catch(Exception ex)
            {

            }

            var JsonData = new
            {
                getUs=getUs
            };
            HttpContext.Current.Response.Write(ser.Serialize(JsonData));
        }
    }
}
