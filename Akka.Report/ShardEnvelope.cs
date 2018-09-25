namespace Akka.Report
{
    public sealed class ShardEnvelope
    {
        public ShardEnvelope(string entityId, object payload)
        {
            EntityId = entityId;
            Payload = payload;
        }

        public string EntityId { get; }
        public object Payload { get; }
    }
}