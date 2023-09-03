using System.Text;
using System.Security.Cryptography;

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

    private static char[] Characters => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
   
    private string ContentIdentifier = string.Empty;

    public Query(IServiceInjector injector) : base(injector) { }

    public async void Process(IConnection connection, object packet) {
        ContentIdentifier = GetContentIdentifier();

        var received = packet as PacketQuery;

        var logger = GetLogger();
        var sender = GetPacketSender();

        var result = new List<string>();

        if (received is not null) {
            var details = Configuration!.Debug;

            WriteReceivedPacket(details, logger, received);

            var results = await Task.Run(() => ExecuteQuery(received));

            WriteQueryResult(details, logger, results);

            GetPacketSender().SendResponse(connection, received, results);
        }
        else {
            logger?.Error(ContentIdentifier, "Packet Query Failed: NULL");
        }
    }

    private List<string> ExecuteQuery(PacketQuery packet) {
        var response = new List<string>();
        var context = GetDatabaseContext(packet);

        if (context is not null) {
            try {
                var error = context.Open();

                if (error.Number != 0) {
                    GetLogger().Error("Error Code", error.Message);
                    GetLogger().Error("Error Message", error.Message);
                }

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

        logger.Error(ContentIdentifier, $"Exception: {ex.Message}");
        logger.Error(ContentIdentifier, $"StackTrace: {ex.StackTrace}");

        logger.Error(ContentIdentifier, $"Query: {packet.Query}");
        logger.Error(ContentIdentifier, $"Database: {GetDatabaseText(packet.Database)}");
        logger.Error(ContentIdentifier, $"Operation: {GetOperationText(packet.Operation)}");
        logger.Error(ContentIdentifier, $"Packet Id: {packet.PacketId}");
        logger.Error(ContentIdentifier, $"Player Index: {packet.Index}");
        logger.Error(ContentIdentifier, $"Connection String: {packet.Connection}");
        logger.Error(ContentIdentifier, $"Field Count: {packet.FieldCount}");
        logger.Error(ContentIdentifier, $"Field Separator: {packet.FieldSeparator}");

        return Task.CompletedTask;
    }

    private void WriteReceivedPacket(bool details, ILogger logger, PacketQuery packet) {
        logger.Info(ContentIdentifier, $"Database: {GetDatabaseText(packet.Database)}");
        logger.Info(ContentIdentifier, $"Operation: {GetOperationText(packet.Operation)}");
        logger.Info(ContentIdentifier, $"Packet Id: {packet.PacketId}");
        logger.Info(ContentIdentifier, $"Player Index: {packet.Index}");

        if (details) {
            logger.Info(ContentIdentifier, $"Field Count: {packet.FieldCount}");
            logger.Info(ContentIdentifier, $"Field Separator: {packet.FieldSeparator}");
            logger.Info(ContentIdentifier, $"Connection String: {packet.Connection}");
            logger.Info(ContentIdentifier, $"Query: {packet.Query}");
        }
    }

    private void WriteQueryResult(bool details, ILogger logger, List<string> results) {
        logger.Info(ContentIdentifier, $"Result Count: {results.Count}");

        if (details) {
            var index = 0;
  
            results.ForEach(x => {
                logger.Info(ContentIdentifier, $"Line {++index}: {x}");
            });
        }
    }

    private ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    private IPacketSender GetPacketSender() {
        return PacketService!.PacketSender!;
    }

    private string GetOperationText(OperationType type) {
        return type == OperationType.ExecuteNonQuery ? "ExecuteNonQuery" : "ExecuteReader";
    }

    private string GetDatabaseText(DatabaseType type) {
        return type == DatabaseType.MySql ? "MySql" : "SqlServer";
    }

    public static string GetContentIdentifier() {
        const int Size = 6;
        const int UIntSize = 4;

        var data = new byte[UIntSize * Size];

        using var crypto = RandomNumberGenerator.Create();

        crypto.GetBytes(data);

        var builder = new StringBuilder(Size);

        for (int i = 0; i < Size; ++i) {
            var rnd = BitConverter.ToUInt32(data, i * UIntSize);
            var idx = rnd % Characters.Length;

            builder.Append(Characters[idx]);
        }

        return builder.ToString();
    }
}