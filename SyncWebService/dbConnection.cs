using System.Data.SqlClient;
using System.Web.Configuration;

namespace SyncWebService
{
    public class dbConnection
    {
        public static SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbCon"].ConnectionString);

        public static void CheckConnectionState()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}