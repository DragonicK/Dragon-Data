using System.Data;
using System.Data.SqlClient;

namespace Dragon.Database.SqlServer;

public sealed class DBConnection : IDBConnection {
    public SqlConnection Connection { get; }

    public DBConnection(string connectionString) {
          Connection = new SqlConnection(connectionString);
    }

    public void Close() {
        Connection?.Close();
    }

    public void Dispose() {
        Connection?.Dispose();
    }

    public DBError Open() {
        var dbError = new DBError();

        try {
            Connection.Open();
        }
        catch (SqlException ex) {
            dbError.Number = ex.Number;
            dbError.Message = ex.Message;
        }

        return dbError;
    }

    public bool IsOpen() {
        return Connection.State == ConnectionState.Open;
    }
}
