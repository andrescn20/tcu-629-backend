
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class DBTest
    {
        readonly static string connectionString = "Server=Andres-HP;Database=TCU-629_local;Trusted_Connection=True;";
        readonly SqlConnection connection;

        public DBTest()
        {
            connection = new SqlConnection(connectionString);
        }

        public void openConnection()
        {
        connection.Open();
        }

         public void closeConnection()
        {
            connection.Close();
        }
        public bool TestConnection()
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception exception)
            {
                return false;
            }

        }
    }
}
