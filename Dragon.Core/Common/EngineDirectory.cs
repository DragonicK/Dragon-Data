namespace Dragon.Core.Common;

public sealed class EngineDirectory {
    private readonly List<string> directory;

    public EngineDirectory() {
        directory = [];
    }

    public void Add(string folder) {
        directory.Add(folder);
    }

    public void Create() {
        directory.ForEach(path => {
            Directory.CreateDirectory(path);
        });
    }
}