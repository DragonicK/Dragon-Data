using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Outgoing;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Relay.Services;

namespace Dragon.Relay.Network;

public sealed class PacketSender : IPacketSender {
    public IOutgoingMessageWriter Writer { get; private set; }
    public OutgoingMessageService? OutgoingMessageService { get; private set; }

    public PacketSender(IServiceInjector injector) {
        injector.Inject(this);

        Writer = OutgoingMessageService!.OutgoingMessageWriter!;
    }

    public void SendResponse(IConnection connection, PacketQuery requested, List<string> content) {
        var index = requested.Index;
        var packetId = requested.PacketId;

        var message = new PacketResponse() {
            Index = index,
            PacketId = packetId,
        };

        if (content.Count > 0) {
            message.Content = new string[content.Count];

            var array = content.ToArray();
            
            array.CopyTo(message.Content, 0);
        }

        var msg = Writer.CreateMessage(message);

        msg.DestinationPeers.Add(connection.Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }
}