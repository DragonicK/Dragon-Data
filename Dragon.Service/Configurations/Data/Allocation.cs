namespace Dragon.Service.Configurations.Data;

public sealed class Allocation {
    public int BufferReaderSize { get; set; }
    public int BufferWriterSize { get; set; }
    public int OutgoingMessageAllocatedSize { get; set; }
    public int IncomingMessageAllocatedSize { get; set; }
}