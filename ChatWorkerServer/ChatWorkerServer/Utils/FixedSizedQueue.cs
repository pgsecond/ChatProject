using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ChatWorkerServer.Utils
{
    public class FixedSizedQueue<T>
    {
        readonly ConcurrentQueue<T> _concurrentQueue = new ConcurrentQueue<T>();
        private readonly object _lockObject = new object();

        public int Limit { get; set; }

        public FixedSizedQueue(int limit)
        {
            Limit = limit;
        }

        public void Enqueue(T obj)
        {
            lock (_lockObject)
            {
                _concurrentQueue.Enqueue(obj);
                while (_concurrentQueue.Count > Limit && _concurrentQueue.TryDequeue(out _)) { }
            }
        }

        public List<T> ToList()
        {
            lock (_lockObject)
            {
                return _concurrentQueue.ToList();
            }
        }
    }
}
