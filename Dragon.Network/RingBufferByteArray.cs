using Dragon.Network.Pool;

namespace Dragon.Network;

public sealed class RingBufferByteArray {
    private const int ListSize = 64;

    public long Sequence { get; set; }
    public int FromId { get; set; }
    public IConnection? Connection { get; set; }
    public TransmissionTarget TransmissionTarget { get; set; }
    public List<int> DestinationPeers { get; set; }
    public int ExceptDestination { get; set; }
    public IBufferReader? BufferReader { get; private set; }
    public IBufferWriter? BufferWriter { get; private set; }

    public RingBufferByteArray() {
        DestinationPeers = new List<int>(ListSize);
    }

    public void SetOutgoingContent(IBufferWriter buffer) => BufferWriter = buffer;

    public void SetIncomingContent(IBufferReader buffer) => BufferReader = buffer;

    public void Reset() {
        Connection = null;
        BufferReader = null;
        BufferWriter = null;

        DestinationPeers.Clear();
    }
}