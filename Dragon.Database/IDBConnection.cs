namespace Dragon.Database;

public interface IDBConnection {
    string Name { get; set; }
    DBError Open();
    void Close();
    void Dispose();
    bool IsOpen();
}