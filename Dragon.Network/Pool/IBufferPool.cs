namespace Dragon.Network.Pool;

public interface IBufferPool {
    IBufferReader GetNextBufferReader();
    IBufferWriter GetNextBufferWriter();
}