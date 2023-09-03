using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

namespace Dragon.Service.Network;

public interface IPacketSender {
    void SendResponse(IConnection connection, PacketQuery requested, List<string> content);
}