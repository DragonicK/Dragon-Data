namespace Dragon.Network;

public sealed class IndexGenerator(int maximum) : IIndexGenerator {
    private readonly HashSet<int> indexes = new(maximum);
    private readonly int Maximum = maximum;

    public int GetNextIndex() {
        for (var i = 1; i <= Maximum; ++i) {
            if (!indexes.Contains(i)) {
                indexes.Add(i);

                return i;
            }
        }

        return 0;
    }

    public void Remove(int index) {
        indexes.Remove(index);
    }
}