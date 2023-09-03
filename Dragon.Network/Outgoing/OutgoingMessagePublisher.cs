namespace Dragon.Network.Outgoing;

public class OutgoingMessagePublisher : IOutgoingMessagePublisher {
    public IConnectionRepository ConnectionRepository { get; }

    public OutgoingMessagePublisher(IConnectionRepository connectionRepository) {
        ConnectionRepository = connectionRepository;
    }

    public void Broadcast(TransmissionTarget peers, IList<int> destination, int exceptDestination, byte[] buffer, int length) {
        IntegerToByteArray(length - 4, buffer, 0);

        switch (peers) {
            case TransmissionTarget.Destination:
                Broadcast(destination, buffer, length);
                break;

            case TransmissionTarget.Broadcast:
                Broadcast(buffer, length);
                break;

            case TransmissionTarget.BroadcastExcept:
                Broadcast(destination, exceptDestination, buffer, length);
                break;
        }
    }

    private void Broadcast(IList<int> destination, int except, byte[] buffer, int length) {
        for (var i = 0; i < destination.Count; i++) {
            var id = destination[i];

            if (except != id) {
                var connection = ConnectionRepository.GetFromId(id);

                if (connection is not null) {
                    if (connection.Connected) {
                        Send(buffer, connection, length);
                    }
                }
            }
        }
    }

    private void Broadcast(IList<int> destination, byte[] buffer, int length) {
        IConnection connection;

        for (var i = 0; i < destination.Count; i++) {
            connection = ConnectionRepository.GetFromId(destination[i]);

            if (connection is not null) {
                if (connection.Connected) {
                    Send(buffer, connection, length);
                }
            }
        }
    }

    private void Broadcast(byte[] buffer, int length) {
        foreach (var (_, connection) in ConnectionRepository) {
            if (connection is not null) {
                if (connection.Connected) {
                    Send(buffer, connection, length);
                }
            }
        }
    }

    private static void Send(byte[] buffer, IConnection connection, int length) {
        connection.Send(buffer, length);
    }

    private static void IntegerToByteArray(int value, byte[] buffer, int offset) {
        buffer[offset] = (byte)(value & 0xFF);
        buffer[offset + 1] = (byte)(value >> 8 & 0xFF);
        buffer[offset + 2] = (byte)(value >> 16 & 0xFF);
        buffer[offset + 3] = (byte)(value >> 24 & 0xFF);
    }
}