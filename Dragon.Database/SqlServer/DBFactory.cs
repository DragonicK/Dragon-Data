namespace Dragon.Database.SqlServer;

public sealed class DBFactory(string connectionString) : IDBFactory {
    private readonly string _conString = connectionString;

    public IDBCommand GetCommand(IDBConnection dbConnection) {
        return new DBCommand(dbConnection);
    }

    public IDBConnection GetConnection() {
        return new DBConnection(_conString);
    }
}