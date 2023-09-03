namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketResponse : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Response;

    /// <summary>
    /// Packet Identification.
    /// </summary>
    public int PacketId { get; set; }

    /// <summary>
    /// Target Index.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Content.
    /// </summary>
    public string[] Content { get; set; } = Array.Empty<string>();
}