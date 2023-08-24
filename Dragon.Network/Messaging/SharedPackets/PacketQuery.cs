
namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketQuery : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Query;
    public string Content { get; set; } = string.Empty;
}