namespace Dragon.Database;

public abstract class DBTemplate {
    public bool Connected {
        get {
            if (sqlConnection != null) {
                return sqlConnection.IsOpen();
            }

            return false;
        }
    }

    protected IDBConnection sqlConnection;
    protected IDBFactory factory;

    public DBTemplate(IDBFactory dbFactory) {
        factory = dbFactory;

        sqlConnection = factory.GetConnection();
    }

    public DBError Open() {
        return sqlConnection.Open();
    }

    public bool IsOpen() {
        return sqlConnection.IsOpen();
    }

    public void Close() {
        sqlConnection.Close();
    }
}