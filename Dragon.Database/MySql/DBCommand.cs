using System.Data;

using MySql.Data.MySqlClient;

namespace Dragon.Database.MySql;

public sealed class DBCommand : IDBCommand {
    private readonly MySqlCommand sqlCommand;
    private readonly DBConnection? sqlConnection;

    public DBCommand(IDBConnection dbConnection) {
        sqlConnection = dbConnection as DBConnection;

        sqlCommand = new MySqlCommand {
            Connection = sqlConnection?.Connection,
            CommandType = CommandType.Text
        };
    }

    public void AddParameter(string parameter, object value) {
        sqlCommand.Parameters.AddWithValue(parameter, value);
    }

    public void SetCommandType(DBCommandType dBCommandType) {
        CommandType command = CommandType.Text;

        switch (dBCommandType) {
            case DBCommandType.Text:
                command = CommandType.Text;
                break;
            case DBCommandType.StoredProcedure:
                command = CommandType.StoredProcedure;
                break;
            case DBCommandType.TableDirect:
                command = CommandType.TableDirect;
                break;
        }

        sqlCommand.CommandType = command;
    }

    public void ClearParameter() {
        sqlCommand.Parameters.Clear();
    }

    public int ExecuteNonQuery() {
        return sqlCommand.ExecuteNonQuery();
    }

    public object ExecuteScalar() {
        return sqlCommand.ExecuteScalar();
    }

    public IDBDataReader ExecuteReader() {
        var reader = sqlCommand.ExecuteReader();

        return new DBDataReader(reader);
    }

    public void SetCommand(string commandText) {
        sqlCommand.CommandText = commandText;
    }
}