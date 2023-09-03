using Dragon.Network;

namespace Dragon.Service.Network;

public interface IPacketRouter {
    void Add(Type key, IPacketRoute value);
    void Process(IConnection connection, object packet);
}