using Dragon.Core.Logs;
using Dragon.Core.Common;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Service.Network;
using Dragon.Service.Database;

namespace Dragon.Service.Routes;

public sealed class Query : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Query;

    public Query(IServiceInjector injector) : base(injector) { }

    public async void Process(IConnection connection, object packet) {
        var received = packet as PacketQuery;

        var logger = GetLogger();
        var sender = GetPacketSender();

        var result = new List<string>();

        if (received is not null) {
            var response = await Task.Run(() => ExecuteQuery(received));

            GetPacketSender().SendResponse(connection, received, response);
        }
        else {
            logger?.Error(GetType().Name, "Packet Query Failed: NULL");
        }
    }

    private List<string> ExecuteQuery(PacketQuery packet) {
        var response = new List<string>();
        var context = GetDatabaseContext(packet);

        if (context is not null) {
            try {
                context.Open();

                if (context.IsOpen()) {
                    response = GetResponseFromDatabase(context, packet);
                }
                else {
                    GetLogger().Error("ExecuteQuery: ", "Connection is not open.");
                }
            }
            catch (Exception ex) {
                WriteExceptionLog(packet, ex);
            }
            finally {
                if (context.IsOpen()) {
                    context.Close();
                }
            }
        }
        else {
            GetLogger().Error("ExecuteQuery: ", "Failed to instantiate context.");
        }
        
        return response;
    }

    private List<string> GetResponseFromDatabase(DatabaseContext context, PacketQuery packet) {
        var query = packet.Query;
        var fields = packet.FieldCount;
        var separator = packet.FieldSeparator;

        if (packet.Operation == OperationType.ExecuteReader) {
            return context!.ExecuteReader(query, separator, fields);
        }
        else if (packet.Operation == OperationType.ExecuteNonQuery) {
            return context!.ExecuteNonQuery(query);
        }

        return new List<string>();
    }

    private DatabaseContext? GetDatabaseContext(PacketQuery packet ) {
        var connection = packet.Connection;

        if (packet.Database == DatabaseType.MySql) {
            return new DatabaseContext(new Dragon.Database.MySql.DBFactory(connection));
        }
        else if (packet.Database == DatabaseType.SqlServer) {
            return new DatabaseContext(new Dragon.Database.SqlServer.DBFactory(connection));
        }

        return null;
    }

    private Task WriteExceptionLog(PacketQuery packet, Exception ex) {
        var logger = GetLogger();

        logger.Error(GetType().Name, $"Exception: {ex.Message}");
        logger.Error(GetType().Name, $"StackTrace: {ex.StackTrace}");

        logger.Error(GetType().Name, $"Query: {packet.Query}");
        logger.Error(GetType().Name, $"Database: {nameof(packet.Database)}");
        logger.Error(GetType().Name, $"Operation: {nameof(packet.Operation)}");
        logger.Error(GetType().Name, $"Index: {packet.Index}");
        logger.Error(GetType().Name, $"Packet Id: {packet.PacketId}");
        logger.Error(GetType().Name, $"Connection String: {packet.Connection}");
        logger.Error(GetType().Name, $"Field Count: {packet.FieldCount}");
        logger.Error(GetType().Name, $"Field Separator: {packet.FieldSeparator}");

        return Task.CompletedTask;
    }

    private ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    private IPacketSender GetPacketSender() {
        return PacketService!.PacketSender!;
    }
}