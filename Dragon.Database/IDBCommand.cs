namespace Dragon.Database;

public interface IDBCommand {
    void AddParameter(string parameter, object value);
    void SetCommand(string commandText);
    void SetCommandType(DBCommandType dBCommandType);
    int ExecuteNonQuery();
    object ExecuteScalar();
    IDBDataReader ExecuteReader();
}