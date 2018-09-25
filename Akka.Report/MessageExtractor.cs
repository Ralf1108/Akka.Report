using Akka.Cluster.Sharding;

namespace Akka.Report
{
    //public sealed class MessageExtractor : IMessageExtractor
    //{
    //    public string EntityId(object message) => (message as ShardEnvelope)?.EntityId.ToString();

    //    public string ShardId(object message) => (message as ShardEnvelope)?.ShardId.ToString();

    //    public object EntityMessage(object message) => (message as ShardEnvelope)?.Message;
    //}

    public sealed class MessageExtractor : HashCodeMessageExtractor
    {
        public MessageExtractor(int maxNumberOfShards)
            : base(maxNumberOfShards)
        {
        }

        public override string EntityId(object message) => (message as ShardEnvelope)?.EntityId;
        public override object EntityMessage(object message) => (message as ShardEnvelope)?.Payload;
    }
}