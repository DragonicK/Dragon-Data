﻿using Dragon.Network;
using Dragon.Network.Outgoing;
using Dragon.Network.Messaging;

using Dragon.Core.Services;

namespace Dragon.Relay.Services;

public sealed class OutgoingMessageService : IService {
    public ServicePriority Priority => ServicePriority.Mid;
    public IOutgoingMessageQueue? OutgoingMessageQueue { get; private set; }
    public IOutgoingMessageEventHandler? OutgoingMessageEventHandler { get; private set; }
    public IOutgoingMessagePublisher? OutgoingMessagePublisher { get; private set; }
    public IOutgoingMessageWriter? OutgoingMessageWriter { get; private set; }
    public ISerializer? Serializer { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PoolService? PoolService { get; private set; }

    public void Start() {
        var repository = ConnectionService!.ConnectionRepository!;
        var bufferPool = PoolService!.EngineBufferPool!;

        Serializer = new MessageSerializer();

        OutgoingMessagePublisher = new OutgoingMessagePublisher(repository);
        OutgoingMessageEventHandler = new OutgoingMessageEventHandler(OutgoingMessagePublisher);
        OutgoingMessageQueue = new OutgoingMessageQueue(OutgoingMessageEventHandler);
        OutgoingMessageWriter = new OutgoingMessageWriter(OutgoingMessageQueue, bufferPool, Serializer);

        OutgoingMessageQueue.Start();
    }

    public void Stop() {
        OutgoingMessageQueue?.Stop();
    }
}