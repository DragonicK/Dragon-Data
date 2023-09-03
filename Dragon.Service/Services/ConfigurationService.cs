using System.Reflection;

using Dragon.Network;

using Dragon.Core.Services;
using Dragon.Core.Serialization;

using Dragon.Service.Configurations;
using Dragon.Service.Configurations.Data;

namespace Dragon.Service.Services;

public sealed class ConfigurationService : IService, IConfiguration {
    public ServicePriority Priority { get; private set; } = ServicePriority.First;
    public bool Debug { get; private set; }
    public IpAddress Server { get; private set; }
    public Allocation Allocation { get; private set; }
    public int MaximumConnections { get; private set; }

    public ConfigurationService() {
        Debug = true;

        Server = new IpAddress() {
            Ip = "0.0.0.0",
            Port = 7002
        };

        Allocation = new Allocation() {
            IncomingMessageAllocatedSize = ushort.MaxValue,
            OutgoingMessageAllocatedSize = ushort.MaxValue,
        };

        MaximumConnections = byte.MaxValue;
    }

    public void Start() {
        const string File = "./Server/Configuration.json";

        if (!Json.FileExists(File)) {
            Json.Save(File, this);
        }
        else {
            var configuration = Json.Get<ConfigurationService>(File);

            if (configuration is not null) {
                InjectObject(configuration);
            }
        }

        Json.Save(File, this);
    }

    public void Stop() {

    }

    private void InjectObject(IConfiguration configuration) {
        var targetType = GetType();
        var properties = targetType.GetRuntimeProperties();

        var pairs = GetProperties(configuration);
        var values = properties.Select(p => p.Name).ToArray();

        foreach (var name in values) {
            var property = properties.Where(p => p.Name == name).First();

            if (pairs.ContainsKey(name)) {
                if (name.CompareTo(nameof(Priority)) != 0) {
                    property.SetValue(this, pairs[name].GetValue(configuration));
                }
            }
        }
    }

    private IDictionary<string, PropertyInfo> GetProperties(IConfiguration configuration) {
        var pairs = new Dictionary<string, PropertyInfo>();
        var properties = configuration.GetType().GetRuntimeProperties();
        var values = properties.Select(p => p.Name).ToArray();

        foreach (var name in values) {
            pairs.Add(name, properties.Where(p => p.Name == name).First());
        }

        return pairs;
    }
}