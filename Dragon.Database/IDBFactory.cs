namespace Dragon.Database;

public interface IDBFactory {
    IDBCommand GetCommand(IDBConnection dbConnection);
    IDBConnection GetConnection();
}