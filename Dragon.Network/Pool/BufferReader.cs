using System.Text;

namespace Dragon.Network.Pool;

public sealed class BufferReader(int size) : IBufferReader { 
    public int Length { get; set; }
    public byte[] Content { get; private set; } = new byte[size];
    public int Capacity => Content.Length;

    int position = 0;

    public void Reset() {
        Length = 0;

        Array.Clear(Content);

        position = 0;
    }

    public void EnsureCapacity(int capacity) {
        Content = new byte[capacity];
    }

    public byte ReadByte() {
        var value = Content[position];

        ++position;

        return value;
    }

    public int ReadInt32() {
        var value = BitConverter.ToInt32(Content, position);

        position += sizeof(int);

        return value;
    }

    public long ReadInt64() {
        var value = BitConverter.ToInt64(Content, position);

        position += sizeof(long);

        return value;
    }

    public short ReadInt16() {
        var value = BitConverter.ToInt16(Content, position);

        position += sizeof(short);

        return value;
    }

    public float ReadFloat() {
        var value = BitConverter.ToSingle(Content, position);

        position += sizeof(float);

        return value;
    }

    public bool ReadBoolean() {
        return ReadByte() == 1;
    }

    public string ReadString() {
        var length = ReadInt32();

        if (length > 0) {
            var text = Encoding.ASCII.GetString(Content, position, length);

            position += length;

            return text;
        }

        return string.Empty;
    }

    public void ResetPosition() {
        position = 0;
    }

    public unsafe void MemoryCopy(void* destination, int destinationSizeInBytes, int sourceBytesToCopy) {
        fixed (byte* p = &Content[position]) {
            Buffer.MemoryCopy(p, destination, destinationSizeInBytes, sourceBytesToCopy);
        }

        position += sourceBytesToCopy;
    }
}