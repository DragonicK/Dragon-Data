using System.Text;

using Dragon.Database;

namespace Dragon.Service.Database;

public sealed class Context : DBTemplate {
    public Context(IDBFactory dBFactory) : base(dBFactory) { }

    public List<string> ExecuteReader(string query, string separator, int fieldCount) {
        var list = new List<string>();
        var line = new StringBuilder();

        var command = factory.GetCommand(sqlConnection);

        command.SetCommand(query);

        var reader = command.ExecuteReader();

        while (reader.Read()) {
            line.Clear();

            for (var i = 0; i < fieldCount; i++) {
                line.Append($"{reader.GetData(i)}{separator}");
            }

            list.Add(line.ToString());
        }

        reader.Close();

        return list;
    }

    public int ExecuteNonQuery(string query) {
        var command = factory.GetCommand(sqlConnection);

        command.SetCommand(query);

        return command.ExecuteNonQuery();
    }
}