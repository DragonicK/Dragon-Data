namespace Dragon.Network.Outgoing;

public class OutgoingMessageWriter(IOutgoingMessageQueue outgoingMessageQueue, ISerializer serializer) : IOutgoingMessageWriter {
    public IOutgoingMessageQueue OutgoingMessageQueue { get; } = outgoingMessageQueue;
    public ISerializer Serializer { get; } = serializer;

    public RingBufferByteArray CreateMessage(object packet) {
        var entry = OutgoingMessageQueue.GetNextSequence();

        entry.SetOutgoingContent(Serializer.Serialize(packet));

        return entry;
    }

    public void Enqueue(RingBufferByteArray buffer) {
        OutgoingMessageQueue.Enqueue(buffer);
    }
}