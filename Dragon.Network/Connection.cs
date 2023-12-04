using System.Net.Sockets;

using Dragon.Core.Logs;

using Dragon.Network.Incoming;

using Dragon.Network.Pool;

namespace Dragon.Network;

public sealed class Connection : IConnection {
    public int Id { get; set; }
    public bool Authenticated { get; set; }
    public string IpAddress { get; set; }
    public Socket? Socket { get; set; }
    public ILogger? Logger { get; set; }
    public bool Connected => connected;
    public IEngineBufferPool? IncomingEngineBufferPool { get; set; }
    public IIncomingMessageQueue? IncomingMessageQueue { get; set; }
    public EventHandler<IConnection>? OnDisconnect { get; set; }

    private const int ReceiveBufferSize = 1024;

    private readonly byte[] buffer;
    private readonly PacketBuffer packet;

    private bool connected = false;

    public Connection() {
        IpAddress = string.Empty;

        buffer = new byte[ReceiveBufferSize];
        packet = new PacketBuffer(ReceiveBufferSize);


        connected = true;
    }

    public void Disconnect() {
        connected = false;

        Socket?.Close();

        OnDisconnect?.Invoke(null, this);
    }

    public void Send(byte[] buffer, int length) {
        Socket?.BeginSend(buffer, 0, length, SocketFlags.None, OnSend, null);
    }

    public void StartBeginReceive() {
        Socket?.BeginReceive(buffer, 0, ReceiveBufferSize, SocketFlags.None, OnReceive, null);
    }

    private void OnReceive(IAsyncResult ar) {
        try {
            var length = Socket!.EndReceive(ar);

            if (length == 0) {
                Disconnect();
            }
            else {
                var pLength = 0;

                packet.Write(buffer, length);

                Array.Clear(buffer, 0, length);

                Logger?.Debug("Received Buffer", $"Length {length}");

                if (packet.Length() >= 4) {
                    pLength = packet.ReadInt32(false);

                    if (pLength <= 0) {
                        return;
                    }
                }

                while (pLength > 0 && pLength <= packet.Length() - 4) {
                    if (pLength <= packet.Length() - 4) {
                        packet.ReadInt32();

                        Logger?.Debug("Received Packet", $"Length {pLength}");

                        var sequence = IncomingEngineBufferPool?.GetNextBufferReader();

                        sequence!.Reset();

                        if (sequence!.Capacity < pLength) {
                            sequence!.EnsureCapacity(pLength);
                        }

                        packet.ReadBytes(sequence.Content, pLength);

                        sequence.Length = pLength;

                        IncomingMessageQueue?.Enqueue(this, Id, sequence);
                    }

                    pLength = 0;

                    if (packet.Length() >= 4) {
                        pLength = packet.ReadInt32(false);

                        if (pLength < 0) {
                            return;
                        }
                    }
                }

                packet.Trim();

                Logger?.Debug("Trim Buffer", $"Length {packet.Length()}");

                Socket.BeginReceive(buffer, 0, ReceiveBufferSize, SocketFlags.None, OnReceive, null);
            }
        }
        catch (Exception ex) {
            Logger?.Write(WarningLevel.Error, GetType().Name, $"OnReceive: {ex.Message}");

            Disconnect();
        }
    }

    private void OnSend(IAsyncResult ar) {
        try {
            Socket?.EndSend(ar);
        }
        catch (Exception ex) {
            Logger?.Write(WarningLevel.Error, GetType().Name, $"OnSend: {ex.Message}");

            Disconnect();
        }
    }
}