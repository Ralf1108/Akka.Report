using System;
using System.Linq;
using Akka.Actor;
using Akka.Util;

namespace Akka.Report
{
    public class QueryHandler : ReceiveActor
    {
        private readonly IActorRef _keyValueTableShard;
        private readonly IActorRef _idResolver;
        private readonly int _maxNumberOfShards;

        private QueryInfo _info;

        public QueryHandler(IActorRef keyValueTableShard, IActorRef idResolver, int maxNumberOfShards)
        {
            _keyValueTableShard = keyValueTableShard;
            _idResolver = idResolver;
            _maxNumberOfShards = maxNumberOfShards;
            Become(State_Ready);
        }

        private void State_Ready()
        {
            Receive<StartQuery>(x =>
            {
                _info = x.QueryInfo;
                _idResolver.Tell(new IdResolver.ResolveKeys(_info.From));
            });

            Receive<IdResolver.KeysResolved>(x =>
            {
                var keys = x.Keys;
                var groupedKeys = keys.ToLookup(HashForShards);

                foreach (var group in groupedKeys)
                {
                    var info = new KeyedQueryInfo(_info.Select, _info.Where, group.ToList());
                    var query = new KeyValueTable.ExecuteQuery(info);
                    var envelope = new ShardEnvelope(group.First().ToString(), query);
                    _keyValueTableShard.Tell(envelope);
                }
            });
        }

        private int HashForShards(Guid key)
        {
            return Math.Abs(MurmurHash.StringHash(key.ToString())) % _maxNumberOfShards;
        }

        public class StartQuery
        {
            public StartQuery(QueryInfo queryInfo)
            {
                QueryInfo = queryInfo;
            }

            public QueryInfo QueryInfo { get; }
        }
    }
}