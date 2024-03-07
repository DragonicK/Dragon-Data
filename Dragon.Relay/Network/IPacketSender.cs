using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

namespace Dragon.Relay.Network;

public interface IPacketSender {
    void SendResponse(IConnection connection, PacketQuery requested, List<string> content);
}