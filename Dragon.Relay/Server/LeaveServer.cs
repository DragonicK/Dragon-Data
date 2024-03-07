using Dragon.Network;

using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Relay.Services;

namespace Dragon.Relay.Server;

public sealed class LeaveServer {
    public LoggerService? LoggerService { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }

    public LeaveServer(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void DisconnectConnection(IConnection connection) {
        var logger = GetLogger();

        var (id, ipAddress) = GetIdAndIpAddress(connection);

        logger?.Info(GetType().Name, $"Disconnected Id: {id} IpAddress: {ipAddress}");

        GetConnectionRepository().RemoveFromId(id);
        GetIndexGenerator().Remove(id);
    }

    public void RefuseConnection(IConnection connection) {
        var logger = GetLogger();

        var (id, ipAddress) = GetIdAndIpAddress(connection);
    
        if (id > 0) {
            GetConnectionRepository().RemoveFromId(id);
            GetIndexGenerator().Remove(id);
        }

        logger?.Info(GetType().Name, $"Refused IpAddress: {ipAddress} Id: {id}");
    }

    private (int id, string ipAddress) GetIdAndIpAddress(IConnection connection) {
        var id = connection is not null ? connection.Id : 0;
        var ipAddress = connection is not null ? connection.IpAddress : string.Empty;

        return (id, ipAddress);
    }

    private ILogger? GetLogger() {
        return LoggerService!.Logger;
    }

    private IIndexGenerator GetIndexGenerator() {
        return ConnectionService!.IndexGenerator!;
    }

    private IConnectionRepository GetConnectionRepository() {
        return ConnectionService!.ConnectionRepository!;
    }
}