namespace Dragon.Database;

public interface IDBConnection {
    DBError Open();
    void Close();
    void Dispose();
    bool IsOpen();
}