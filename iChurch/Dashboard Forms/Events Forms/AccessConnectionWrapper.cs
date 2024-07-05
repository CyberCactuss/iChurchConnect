using iChurch.DBAccess.Connection;
using System.Data.OleDb;

public class AccessConnectionWrapper : IDisposable
{
    private AccessConnection connection;

    public AccessConnectionWrapper()
    {
        connection = new AccessConnection();
        connection.OpenConnection();
    }

    public OleDbConnection GetConnection()
    {
        return connection.GetConnection();
    }

    public void Dispose()
    {
        if (connection != null)
        {
            connection.CloseConnection();
            connection = null;
        }
    }
}
