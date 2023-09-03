using Dragon.Network;

using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Service.Services;

namespace Dragon.Service.Server;

public sealed class JoinServer {
    public LoggerService? LoggerService { get; private set; }
    public OutgoingMessageService? OutgoingMessageService { get; private set; }

    public JoinServer(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void AcceptConnection(IConnection connection) {
        var logger = GetLogger();

        var id = connection is not null ? connection.Id : 0;
        var ipAddress = connection is not null ? connection.IpAddress : string.Empty;

        logger?.Info(GetType().Name, $"Approval Id: {id} IpAddress: {ipAddress}");
    }

    private ILogger? GetLogger() {
        return LoggerService!.Logger;
    }
}