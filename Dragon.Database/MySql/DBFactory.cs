namespace Dragon.Database.MySql;

public sealed class DBFactory : IDBFactory {
    private readonly string _conString;

    public DBFactory(string connectionString) {
        _conString = connectionString;
    }

    public IDBCommand GetCommand(IDBConnection dBConnection) {
        return new DBCommand(dBConnection);
    }

    public IDBConnection GetConnection() {
        return new DBConnection(_conString);
    }
}