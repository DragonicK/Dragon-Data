namespace Dragon.Database.MySql;

public sealed class DBFactory : IDBFactory {
    readonly DBConfiguration configuration;

    public DBFactory(DBConfiguration dBConfiguration) {
        configuration = dBConfiguration;
    }

    public IDBCommand GetCommand(IDBConnection dBConnection) {
        return new DBCommand(dBConnection);
    }

    public IDBConnection GetConnection() {
        return new DBConnection(configuration);
    }
}