namespace Dragon.Database.SqlServer;

public sealed class DBFactory : IDBFactory {
    private readonly string _conString;

    public DBFactory(string connectionString) {
        _conString = connectionString;
    }

    public IDBCommand GetCommand(IDBConnection dbConnection) {
        return new DBCommand(dbConnection);
    }

    public IDBConnection GetConnection() {
        return new DBConnection(_conString);
    }
}