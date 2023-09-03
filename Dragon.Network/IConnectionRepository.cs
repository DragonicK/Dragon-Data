using System.Collections.Concurrent;

namespace Dragon.Network;

public interface IConnectionRepository {
    ConcurrentDictionary<int, IConnection> Connections { get; }
    IConnection AddClientFromId(int connectionId);
    IConnection GetFromId(int connectionId);
    IConnection RemoveFromId(int connectionId);
    bool Contains(int connectionId);
    void Clear();
    int Count();
    void Disconnect();
    IEnumerator<KeyValuePair<int, IConnection>> GetEnumerator();
}