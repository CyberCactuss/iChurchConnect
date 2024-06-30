using System;
using System.Data;
using System.Data.OleDb;

namespace iChurch.DBAccess.Authentication
{
    internal class AccessAuthentication
    {
        private OleDbConnection connection;

        public AccessAuthentication(OleDbConnection dbConnection)
        {
            connection = dbConnection;
        }

        public bool CheckCredentials(string username, string password)
        {
            bool isValid = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "SELECT Count(*) FROM Admin WHERE Username = @username AND Password = @password";
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        isValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error checking credentials: " + ex.Message);
                isValid = false; 
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return isValid;
        }
    }
}
