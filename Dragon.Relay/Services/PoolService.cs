using Dragon.Core.Services;

using Dragon.Network.Pool;

namespace Dragon.Relay.Services;

public sealed class PoolService : IService {
    public ServicePriority Priority => ServicePriority.First;
    public ConfigurationService? Configuration { get; private set; }
    public IBufferPool? EngineBufferPool { get; private set; }
    public LoggerService? LoggerService { get; private set; }

    public void Start() {
        var allocated = Configuration!.Allocation;

        var readerSize = allocated.BufferReaderSize;
        var writerSize = allocated.BufferWriterSize;

        var outgoingSize = allocated.OutgoingMessageAllocatedSize;
        var incomingSize = allocated.IncomingMessageAllocatedSize;

        EngineBufferPool = new BufferPool(incomingSize, outgoingSize, readerSize, writerSize);

        var logger = LoggerService?.Logger;

        logger?.Info("Reader Buffer Size", readerSize.ToString());
        logger?.Info("Writer Buffer Size", writerSize.ToString());
        logger?.Info("Outgoing Message Pool Size", outgoingSize.ToString());
        logger?.Info("Incoming Message Pool Size", incomingSize.ToString());
    }

    public void Stop() {

    }
}