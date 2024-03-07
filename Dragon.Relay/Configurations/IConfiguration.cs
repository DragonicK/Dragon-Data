using Dragon.Network;

using Dragon.Relay.Configurations.Data;

namespace Dragon.Relay.Configurations;

public interface IConfiguration {
    bool Debug { get; }
    IpAddress Server { get; }
    Allocation Allocation { get; }
    int MaximumConnections { get; }
}