using System;
using System.Collections.Generic;
using Akka.Actor;

namespace Akka.Report
{
    public class KeyValueTable : ReceiveActor
    {
        private readonly Dictionary<Guid, RowState> _loadedRows = new Dictionary<Guid, RowState>();

        public KeyValueTable()
        {
            Become(State_Ready);
        }

        private void State_Ready()
        {
            Receive<ExecuteQuery>(x =>
            {
                foreach (var key in x.Info.From)
                {
                    if (_loadedRows.TryGetValue(key, out var row))
                    {
                        // missing -> dynamic ColumnName to Index assignment
                        // persist tables to disk
                            // store meta file with version number in file name, write with atomic because shared by shards -> metadata writer actor
                            // store all content data in single files with meta data version in filename -> separate actor with flush
                            // use filesystemwatcher
                            // cleanup job for meta files, if version number not in use -> remove
                            // only load data which is required / asked for
                            // use meta data files to quickly determine if data can be loaded from disk
                            // keep files up to date via touch / last accessed timestamp if possible

                    }
                }
            });
        }

        public class ExecuteQuery
        {
            public ExecuteQuery(KeyedQueryInfo info)
            {
                Info = info;
            }

            public KeyedQueryInfo Info { get; }
        }
    }
}