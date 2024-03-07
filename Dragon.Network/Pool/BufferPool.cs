namespace Dragon.Network.Pool;

public sealed class BufferPool : IBufferPool {
    private int rIndex;
    private int wIndex;

    private readonly List<IBufferReader> readers;
    private readonly List<IBufferWriter> writers;

    private readonly object _rLock = new();
    private readonly object _wLock = new();

    public BufferPool(int readerLength, int writerLength, int bufferReaderSize, int bufferWriterSize) {
        readers = new List<IBufferReader>(readerLength);
        writers = new List<IBufferWriter>(writerLength);

        for (var i = 0; i < readerLength; ++i) {
            readers.Add(new BufferReader(bufferReaderSize));
        }

        for (var i = 0; i < writerLength; ++i) {
            writers.Add(new BufferWriter(bufferWriterSize));
        }
    }

    public IBufferReader GetNextBufferReader() {
        lock (_rLock) {
            rIndex = rIndex >= readers.Count ? 0 : rIndex++;

            return readers[rIndex];
        }
    }

    public IBufferWriter GetNextBufferWriter() {
        lock (_wLock) {
            wIndex = wIndex >= writers.Count ? 0 : wIndex++;

            return writers[wIndex];
        }
    }
}