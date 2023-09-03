using Dragon.Network;

using Dragon.Service.Configurations.Data;

namespace Dragon.Service.Configurations;

public interface IConfiguration {
    bool Debug { get; }
    IpAddress Server { get; }
    Allocation Allocation { get; }
    int MaximumConnections { get; }
}