namespace Dragon.Database.MySql;

public sealed class DBFactory(string connectionString) : IDBFactory {
    private readonly string _conString = connectionString;

    public IDBCommand GetCommand(IDBConnection dBConnection) {
        return new DBCommand(dBConnection);
    }

    public IDBConnection GetConnection() {
        return new DBConnection(_conString);
    }
}