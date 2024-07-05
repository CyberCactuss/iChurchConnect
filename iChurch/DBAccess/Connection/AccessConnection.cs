using System.Data.OleDb;

namespace iChurch.DBAccess.Connection
{
    public class AccessConnection
    {
        private OleDbConnection connection;
        private string databaseFile = @"..\..\..\..\Database\iChurchConnect.accdb";
        
        public AccessConnection()
        {
            string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFile};";
            connection = new OleDbConnection(connectionString);
        }

        // Methods for opening, closing, and retrieving the connection
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public OleDbConnection GetConnection()
        {
            return connection;
        }
    }
}
