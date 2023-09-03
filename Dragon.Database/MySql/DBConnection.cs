using System.Data;

using MySql.Data.MySqlClient;

namespace Dragon.Database.MySql;

public sealed class DBConnection : IDBConnection {
    public MySqlConnection Connection { get; set; }

    private readonly string _connectionString;

    public DBConnection(string connectionString) {
        _connectionString = connectionString;

        Connection = new MySqlConnection();
    }

    public DBError Open() {
        var dbError = new DBError();

        Connection.ConnectionString = _connectionString;

        try {
            Connection.Open();
        }
        catch (MySqlException ex) {
            // O connector do MySQL não retorna o número do erro.
            // Nesse caso, estou usando o tamanho da mensagem para indicar que há um erro.
            dbError.Number = ex.Message.Length;
            dbError.Message = ex.Message;
        }

        return dbError;
    }

    public void Close() {
        Connection?.Close();
    }

    public void Dispose() {
        Connection?.Dispose();
    }

    public bool IsOpen() {
        return Connection.State == ConnectionState.Open;
    }
}