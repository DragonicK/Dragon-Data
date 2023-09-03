using Dragon.Core.Services;

namespace Dragon.Service.Network;

public sealed class PacketSender : IPacketSender {
    public PacketSender(IServiceInjector injector) {
        injector.Inject(this);
    }
}