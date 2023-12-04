using System.Net.Sockets;

using Dragon.Network.Pool;
using Dragon.Network.Incoming;

namespace Dragon.Network;

public interface IConnection {
    int Id { get; set; }
    bool Connected { get; }
    Socket? Socket { get; set; }
    string IpAddress { get; set; }
    bool Authenticated { get; set; }
    IEngineBufferPool? IncomingEngineBufferPool { get; set; }
    IIncomingMessageQueue? IncomingMessageQueue { get; set; }
    EventHandler<IConnection>? OnDisconnect { get; set; }

    void StartBeginReceive();
    void Disconnect();
    void Send(byte[] buffer, int length);
}