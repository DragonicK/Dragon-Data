using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Service.Network;

namespace Dragon.Service.Routes;

public sealed class Ping(IServiceInjector injector) : PacketRoute(injector), IPacketRoute {
    public MessageHeader Header => MessageHeader.Ping;

    public void Process(IConnection connection, object packet) {

    }
}