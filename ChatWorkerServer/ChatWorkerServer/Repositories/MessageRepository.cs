using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ChatWorkerServer.Interfaces;
using JsonFlatFileDataStore;
using Shared.Models;

namespace ChatWorkerServer.Repositories
{
    public class MessageRepository<T> : IDataRepository<T> where T : Message
    {
        private readonly DataStore _dataStore = new DataStore(Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location) + "\\MessageDB.json");

        public IEnumerable<T> GetAll() => _dataStore.GetCollection<T>().AsQueryable();

        public async void AddRangeAsync(List<T> list)
        {
            IDocumentCollection<T> collection = _dataStore.GetCollection<T>();
            foreach (var item in list)
            {
                await collection.InsertOneAsync(item);
            }
        }

        public async Task<bool> AddAsync(T item) => await _dataStore.GetCollection<T>().InsertOneAsync(item);
    }
}
