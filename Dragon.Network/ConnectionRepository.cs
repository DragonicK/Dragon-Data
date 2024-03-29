﻿using Dragon.Core.Logs;

using System.Collections.Concurrent;

namespace Dragon.Network;

public sealed class ConnectionRepository(ILogger logger) : IConnectionRepository {
    public ConcurrentDictionary<int, IConnection> Connections { get; private set; } = new ConcurrentDictionary<int, IConnection>();

    private readonly ILogger _logger = logger;

    public IConnection AddClientFromId(int connectionId) {
        if (!Connections.ContainsKey(connectionId)) {
            Connections.TryAdd(connectionId, new Connection { Id = connectionId, Logger = _logger });
        }

        return Connections.GetOrAdd(connectionId, default(IConnection)!);
    }

    public IConnection GetFromId(int connectionId) {
        return Connections.GetOrAdd(connectionId, default(IConnection)!);
    }

    public IConnection RemoveFromId(int connectionId) {
        Connections.TryRemove(connectionId, out var connection);

        return connection;
    }

    public bool Contains(int connectionId) {
        return Connections.ContainsKey(connectionId);
    }

    public void Clear() {
        Connections.Clear();
    }

    public int Count() {
        return Connections.Count;
    }

    public void Disconnect() {
        Connections.Select(x => x.Value).ToList().ForEach(x => x.Disconnect());
    }

    public IEnumerator<KeyValuePair<int, IConnection>> GetEnumerator() {
        return Connections.GetEnumerator();
    }
}