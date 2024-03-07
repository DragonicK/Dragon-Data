using Dragon.Network.Pool;

namespace Dragon.Network.Outgoing;

public class OutgoingMessagePublisher(IConnectionRepository connectionRepository) : IOutgoingMessagePublisher {
    public IConnectionRepository ConnectionRepository { get; } = connectionRepository;

    public void Broadcast(TransmissionTarget peers, IList<int> destination, int exceptDestination, IBufferWriter buffer) {
        IntegerToByteArray(buffer.Length - 4, buffer.Content, 0);

        switch (peers) {
            case TransmissionTarget.Destination:
                Broadcast(destination, buffer);
                break;

            case TransmissionTarget.Broadcast:
                Broadcast(buffer);
                break;

            case TransmissionTarget.BroadcastExcept:
                Broadcast(destination, exceptDestination, buffer);
                break;
        }
    }

    private void Broadcast(IList<int> destination, int except, IBufferWriter buffer) {
        for (var i = 0; i < destination.Count; i++) {
            var id = destination[i];

            if (except != id) {
                var connection = ConnectionRepository.GetFromId(id);

                if (connection is not null) {
                    if (connection.Connected) {
                        Send(connection, buffer);
                    }
                }
            }
        }
    }

    private void Broadcast(IList<int> destination, IBufferWriter buffer) {
        IConnection connection;

        for (var i = 0; i < destination.Count; i++) {
            connection = ConnectionRepository.GetFromId(destination[i]);

            if (connection is not null) {
                if (connection.Connected) {
                    Send(connection, buffer);
                }
            }
        }
    }

    private void Broadcast(IBufferWriter buffer) {
        foreach (var (_, connection) in ConnectionRepository) {
            if (connection is not null) {
                if (connection.Connected) {
                    Send(connection, buffer);
                }
            }
        }
    }

    private static void Send(IConnection connection, IBufferWriter buffer) {
        connection.Send(buffer.Content, buffer.Length);
    }

    private static void IntegerToByteArray(int value, byte[] buffer, int offset) {
        buffer[offset] = (byte)(value & 0xFF);
        buffer[offset + 1] = (byte)(value >> 8 & 0xFF);
        buffer[offset + 2] = (byte)(value >> 16 & 0xFF);
        buffer[offset + 3] = (byte)(value >> 24 & 0xFF);
    }
}