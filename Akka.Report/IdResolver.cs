using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace Akka.Report
{
    public class IdResolver : ReceiveActor
    {
        public IdResolver()
        {
            Receive<ResolveKeys>(x =>
            {
                var resolvedKeys = x.Keys
                    .Select(k => new Guid(Convert.ToInt32(k), 0, 0, new byte[8]))
                    .ToList();

                Sender.Tell(new KeysResolved(resolvedKeys));
            });
        }

        public class ResolveKeys
        {
            public ResolveKeys(List<string> keys)
            {
                Keys = keys;
            }

            public List<string> Keys { get; }
        }

        public class KeysResolved
        {
            public KeysResolved(List<Guid> keys)
            {
                Keys = keys;
            }

            public List<Guid> Keys { get; }
        }
    }
}