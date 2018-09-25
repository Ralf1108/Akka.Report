using Akka.Actor;
using Akka.Cluster.Sharding;

namespace Akka.Report
{
    public class Manager : ReceiveActor
    {
        private int _maxNumberOfShards = 10;

        private readonly IActorRef _keyValueTableShard;
        private readonly IActorRef _idResolver;

        public Manager()
        {
            _keyValueTableShard = ClusterSharding.Get(Context.System).Start(
                "KeyValueTable",
                Props.Create<KeyValueTable>(),
                ClusterShardingSettings.Create(Context.System),
                new MessageExtractor(_maxNumberOfShards));

            _idResolver = Context.ActorOf<IdResolver>();

            Become(State_Ready);
        }

        private void State_Ready()
        {
            Receive<Query>(x =>
            {
                var handler = Context.ActorOf(Props.Create(() => new QueryHandler(_keyValueTableShard, _idResolver, _maxNumberOfShards)));
                Context.Watch(handler);

                handler.Tell(new QueryHandler.StartQuery(x.QueryInfo));
            });
        }

        public class Query
        {
            public QueryInfo QueryInfo { get; set; }
        }
    }
}