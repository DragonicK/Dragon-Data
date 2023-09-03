using Dragon.Core.Common;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketQuery : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Query;

    /// <summary>
    /// Packet Identification.
    /// </summary>
    public int PacketId { get; set; }

    /// <summary>
    /// Target Index.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Database Type - MySql / Sql Server.
    /// </summary>
    public DatabaseType Database { get; set; }

    /// <summary>
    /// Operation Type - Select, Insert, Update, Delete.
    /// </summary>
    public OperationType Operation { get; set; }

    /// <summary>
    /// Query fields count when reading from a table.
    /// </summary>
    public int FieldCount { get; set; }

    /// <summary>
    /// Field Seperator.
    /// </summary>
    public string FieldSeparator { get; set; } = string.Empty;

    /// <summary>
    /// Dabase Connection String.
    /// </summary>
    public string Connection { get; set; } = string.Empty;

    public string Query { get; set; } = string.Empty;
}