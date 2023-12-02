using System.Data.SqlClient;

namespace Dragon.Database.SqlServer;

public sealed class DBDataReader(SqlDataReader sqlDataReader) : IDBDataReader {
    private readonly SqlDataReader sqlReader = sqlDataReader;

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