using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataGenerator
{
    public interface IRandomDataGenerator<T>
    {
        IEnumerable<T> CreateMany(int numOfObjects);
        Task<List<T>> CreateManyAsync(int numOfObjects, CancellationToken token);
    }
}