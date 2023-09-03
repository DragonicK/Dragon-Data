using System.Data;
using System.Data.SqlClient;

namespace Dragon.Database.SqlServer;

public sealed class DBConnection : IDBConnection {
    public string Name { get; set; }
    public SqlConnection Connection { get; }

    public DBConnection(DBConfiguration dBConfiguration) {
        Name = dBConfiguration.Name;

        var sqlConnectionString = new SqlConnectionStringBuilder() {
            DataSource = dBConfiguration.DataSource,
            InitialCatalog = dBConfiguration.Database,
            Password = dBConfiguration.Password,
            UserID = dBConfiguration.UserId,
            Pooling = true,
            MinPoolSize = dBConfiguration.MinPoolSize,
            MaxPoolSize = dBConfiguration.MaxPoolSize
        };

        Connection = new SqlConnection(sqlConnectionString.ToString());
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
