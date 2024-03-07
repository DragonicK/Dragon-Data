namespace Dragon.Network.Pool;

public interface IBufferWriter {
    int Length { get; }
    int Capacity { get; }
    byte[] Content { get; }
    void Reset();
    void Write(byte value);
    void Write(bool value);
    void Write(short value);
    void Write(int value);
    void Write(long value);
    void Write(float value);
    void Write(string value);
    void WriteEmptyBytes(int count);
    void EnsureCapacity(int capacity);
}