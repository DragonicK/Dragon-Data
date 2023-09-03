using System.Text;

using Dragon.Database;

namespace Dragon.Service.Database;

public sealed class DatabaseContext : DBTemplate {
    public DatabaseContext(IDBFactory dBFactory) : base(dBFactory) { }

    public List<string> ExecuteReader(string query, string separator, int fieldCount) {
        var list = new List<string>();
        var line = new StringBuilder();

        var command = factory.GetCommand(sqlConnection);

        command.SetCommand(query);

        var reader = command.ExecuteReader();

        while (reader.Read()) {
            line.Clear();

            for (var i = 0; i < fieldCount; ++i) {
                line.Append($"{reader.GetData(i)}{separator}");
            }

            list.Add(line.ToString());
        }

        reader.Close();

        return list;
    }

    public List<string> ExecuteNonQuery(string query) {
        var list = new List<string>();  

        var command = factory.GetCommand(sqlConnection);

        command.SetCommand(query);

        list.Add(command.ExecuteNonQuery().ToString());

        return list;
    }
}