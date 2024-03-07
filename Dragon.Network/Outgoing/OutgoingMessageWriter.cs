using Dragon.Network.Pool;

namespace Dragon.Network.Outgoing;

public class OutgoingMessageWriter : IOutgoingMessageWriter {
    public IOutgoingMessageQueue OutgoingMessageQueue { get; } 
    public IBufferPool BufferPool { get; } 
    public ISerializer Serializer { get; } 

    public OutgoingMessageWriter(IOutgoingMessageQueue outgoingMessageQueue, IBufferPool bufferPool, ISerializer serializer) {
        OutgoingMessageQueue = outgoingMessageQueue;
        BufferPool = bufferPool;
        Serializer = serializer;
    }

    public RingBufferByteArray CreateMessage(object packet) {
        var entry = OutgoingMessageQueue.GetNextSequence();

        var sequence = BufferPool.GetNextBufferWriter();

        sequence.Reset();

        sequence = Serializer.Serialize(packet, sequence);

        entry.SetOutgoingContent(sequence);

        return entry;
    }

    public void Enqueue(RingBufferByteArray buffer) {
        OutgoingMessageQueue.Enqueue(buffer);
    }
}