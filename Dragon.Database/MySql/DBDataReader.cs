using MySql.Data.MySqlClient;

namespace Dragon.Database.MySql;

public sealed class DBDataReader(MySqlDataReader reader) : IDBDataReader {
    private readonly MySqlDataReader sqlReader = reader;

    public void Close() {
        sqlReader.Close();
        sqlReader.Dispose();
    }

    public bool Read() {
        return sqlReader.Read();
    }

    public object GetData(string column) {
        return sqlReader[column];
    }

    public object GetData(int column) {
        return sqlReader[column];
    }
}