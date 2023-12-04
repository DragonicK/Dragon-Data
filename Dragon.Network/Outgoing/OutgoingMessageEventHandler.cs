namespace Dragon.Network.Outgoing;
public class OutgoingMessageEventHandler(IOutgoingMessagePublisher outgoingMessagePublisher) : IOutgoingMessageEventHandler {

    public IOutgoingMessagePublisher OutgoingMessagePublisher { get; } = outgoingMessagePublisher;

    public void OnEvent(RingBufferByteArray buffer, long sequence, bool endOfBatch) {
        OutgoingMessagePublisher.Broadcast(
            buffer.TransmissionTarget,
            buffer.DestinationPeers,
            buffer.ExceptDestination,
            buffer.BufferWriter!
        );

        buffer.Reset();
    }
}