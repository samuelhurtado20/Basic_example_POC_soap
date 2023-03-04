using Basic_example_POC_soap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;

namespace Basic_example_POC_soap
{
    /// <summary>
    /// Summary description for DemoSoap
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DemoSoap : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public ResponseModel<string> login(string email, string password)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            if (email != null)
            {
                using (SqlConnection conn = new SqlConnection(@"Server=SAMUELHURTADO\SQLEXPRESS;Database=IdentityServer4;Trusted_Connection=True;"))
                {
                    SqlCommand cmd = new SqlCommand("sp_loginUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password;


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        response.Data = JsonConvert.SerializeObject(dt);
                        response.resultCode = 200;
                    }
                    else
                    {
                        response.message = "User Not Found!";
                        response.resultCode = 500;
                    }


                }
            }
            return response;
        }
    }
}
