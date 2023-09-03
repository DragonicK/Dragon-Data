using Dragon.Core.Services;

using Dragon.Service.Network;

namespace Dragon.Service.Services;

public sealed class PacketService : IService {
    public ServicePriority Priority => ServicePriority.Mid;
    public IPacketSender? PacketSender { get; private set; }
    public IServiceInjector? ServiceInjector { get; private set; }

    public void Start() {
        PacketSender = new PacketSender(ServiceInjector!);
    }

    public void Stop() {

    }
}