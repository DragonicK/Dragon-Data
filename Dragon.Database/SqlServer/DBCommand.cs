using System.Data;

using System.Data.SqlClient;

namespace Dragon.Database.SqlServer;

public sealed class DBCommand : IDBCommand {
    private readonly SqlCommand sqlCommand;
    private readonly DBConnection? sqlConnection;

    public DBCommand(IDBConnection dbConnection) {
        sqlConnection = dbConnection as DBConnection;

        sqlCommand = new SqlCommand {
            Connection = sqlConnection?.Connection,
            CommandType = CommandType.Text
        };
    }

    public void AddParameter(string parameter, object value) {
        sqlCommand.Parameters.AddWithValue(parameter, value);
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

    public void SetCommandType(DBCommandType dBCommandType) {
        var command = CommandType.Text;

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
}
