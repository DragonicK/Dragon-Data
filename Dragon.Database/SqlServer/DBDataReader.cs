using System.Data.SqlClient;

namespace Dragon.Database.SqlServer;

public sealed class DBDataReader : IDBDataReader {
    private readonly SqlDataReader sqlReader;

    public DBDataReader(SqlDataReader sqlDataReader) {
        sqlReader = sqlDataReader;
    }

    public void Close() {
        sqlReader.Close();
    }

    public object GetData(string column) {
        return sqlReader[column];
    }

    public object GetData(int column) {
        return sqlReader[column];
    }

    public bool Read() {
        return sqlReader.Read();
    }
}