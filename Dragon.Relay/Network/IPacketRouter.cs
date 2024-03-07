using Dragon.Network;

namespace Dragon.Relay.Network;

public interface IPacketRouter {
    void Add(Type key, IPacketRoute value);
    void Process(IConnection connection, object packet);
}