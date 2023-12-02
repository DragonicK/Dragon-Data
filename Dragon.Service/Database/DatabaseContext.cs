using System.Text;

using Dragon.Database;

namespace Dragon.Service.Database;

public sealed class DatabaseContext(IDBFactory dBFactory) : DBTemplate(dBFactory) {
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

            list.Add(GetTextWithoutLastSeparator(line));
        }

        reader.Close();

        return list;
    }

    private static string GetTextWithoutLastSeparator(StringBuilder builder) {
        if (builder.Length > 0) {
            return builder.Remove(builder.Length - 1, 1).ToString();
        }

        return string.Empty;
    }

    public List<string> ExecuteNonQuery(string query) {
        var list = new List<string>();  

        var command = factory.GetCommand(sqlConnection);

        command.SetCommand(query);

        list.Add(command.ExecuteNonQuery().ToString());

        return list;
    }
}