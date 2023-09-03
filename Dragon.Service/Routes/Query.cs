using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Service.Network;

namespace Dragon.Service.Routes;

public sealed class Query : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Query;

    public Query(IServiceInjector injector) : base(injector) { }

    public async void Process(IConnection connection, object packet) {
        var received = packet as PacketQuery;
        var logger = GetLogger();

        if (received is not null) {

        }
        else {
            logger?.Error(GetType().Name, "Packet Failed: Authentication Login");
        }
    }

    private Task WriteExceptionLog(PacketQuery packet, Exception ex) {
        var logger = GetLogger();

        //logger.Error(GetType().Name, $"An error ocurred by {username} ... {message}");

        return Task.CompletedTask;
    }

    private ILogger GetLogger() {
        return LoggerService!.Logger!;
    }
}