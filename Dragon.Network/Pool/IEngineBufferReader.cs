namespace Dragon.Network.Pool;

public interface IEngineBufferReader {
    int Capacity { get; }
    byte[] Content { get; }
    int Length { get; set; }
    void Reset();
    byte ReadByte();
    int ReadInt32();
    long ReadInt64();
    short ReadInt16();
    float ReadFloat();
    bool ReadBoolean();
    string ReadString();
    void ResetPosition();
    void EnsureCapacity(int capacity);
    unsafe void MemoryCopy(void* destination, int destinationSizeInBytes, int sourceBytesToCopy);
}