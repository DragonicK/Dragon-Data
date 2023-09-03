﻿using Dragon.Core.Logs;

using Dragon.Network;

namespace Dragon.Service.Network;

public sealed class PacketRouter : IPacketRouter {
    private readonly Dictionary<Type, IPacketRoute> routes;
    private readonly ILogger _logger;

    public PacketRouter(ILogger logger) {
        routes = new Dictionary<Type, IPacketRoute>();
        _logger = logger;
    }

    public void Add(Type key, IPacketRoute value) => routes.Add(key, value);

    private bool Contains(object packet) => routes.ContainsKey(packet.GetType());

    public void Process(IConnection connection, object packet) {
        if (Contains(packet)) {
            var route = routes[packet.GetType()];

            route.Process(connection, packet);
        }
    }
}