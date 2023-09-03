using MySql.Data.MySqlClient;

namespace Dragon.Database.MySql;

public sealed class DBDataReader : IDBDataReader {
    private readonly MySqlDataReader sqlReader;

    public DBDataReader(MySqlDataReader reader) {
        sqlReader = reader;
    }

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