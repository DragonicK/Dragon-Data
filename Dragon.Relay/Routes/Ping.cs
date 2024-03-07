using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Relay.Network;

namespace Dragon.Relay.Routes;

public sealed class Ping(IServiceInjector injector) : PacketRoute(injector), IPacketRoute {
    public MessageHeader Header => MessageHeader.Ping;

    public void Process(IConnection connection, object packet) {

    }
}