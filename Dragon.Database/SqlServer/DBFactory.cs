namespace Dragon.Database.SqlServer;

public sealed class DBFactory : IDBFactory {
    readonly DBConfiguration configuration;

    public DBFactory(DBConfiguration dBConfiguration) {
        configuration = dBConfiguration;
    }

    public IDBCommand GetCommand(IDBConnection dbConnection) {
        return new DBCommand(dbConnection);
    }

    public IDBConnection GetConnection() {
        return new DBConnection(configuration);
    }
}