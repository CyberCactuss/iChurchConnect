using System;
using System.Data.OleDb;

namespace iChurch.DBAccess.Connection
{
    public class AccessConnection : IDisposable
    {
        private OleDbConnection connection;
        private string databaseFile = @"..\..\..\..\Database\iChurchConnect.accdb";

        public AccessConnection()
        {
            string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFile};";
            connection = new OleDbConnection(connectionString);
        }

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

        public void Dispose()
        {
            CloseConnection();
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}
