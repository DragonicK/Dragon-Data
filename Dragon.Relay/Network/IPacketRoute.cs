using Dragon.Network;
using Dragon.Network.Messaging;

namespace Dragon.Relay.Network;

public interface IPacketRoute {
    MessageHeader Header { get; }
    void Process(IConnection connection, object packet);
}