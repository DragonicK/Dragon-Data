namespace Dragon.Database;

public sealed class DBConfiguration {
    public int Port { get; set; }
    public string Name { get; set; } 
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }
    public int MinPoolSize { get; set; }
    public int MaxPoolSize { get; set; }
    public string DataSource { get; set; }

    public DBConfiguration() {
        Name = string.Empty;
        UserId = string.Empty;
        Password = string.Empty; 
        Database = string.Empty;
        DataSource = string.Empty;
    }
}