namespace Dragon.Database;

public interface IDBDataReader {
    void Close();
    bool Read();
    object GetData(int column);
    object GetData(string column);
}