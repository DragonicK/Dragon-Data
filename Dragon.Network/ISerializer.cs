using Dragon.Network.Pool;

namespace Dragon.Network;

public interface ISerializer {
    public IBufferWriter Serialize<T>(T type, IBufferWriter buffer);
    object Deserialize(IBufferReader buffer, Type type);
}