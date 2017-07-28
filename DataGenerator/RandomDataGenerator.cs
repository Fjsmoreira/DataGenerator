using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace DataGenerator
{
    public class RandomDataGenerator<T> : IRandomDataGenerator<string>
    {
        private ISpecimenBuilder specimenBuilder;
        private IStringGenerator stringGenerator;
        public RandomDataGenerator(ISpecimenBuilder specimenBuilder, IStringGenerator stringGenerator)
        {
            EnsureArg.IsNotNull(specimenBuilder,nameof(specimenBuilder));
            EnsureArg.IsNotNull(stringGenerator, nameof(stringGenerator));

            this.stringGenerator = stringGenerator;
            this.specimenBuilder = specimenBuilder;
        }

        public IEnumerable<string> CreateMany(int numOfObjects)
        {
            var fixtureData = specimenBuilder.CreateMany<T>(numOfObjects);

            foreach (var data in fixtureData)
            {
                yield return stringGenerator.Generate(data);
            }

        }

        public Task<List<string>> CreateManyAsync(int numOfObjects, CancellationToken token)
        {
            return Task.Run( () =>
            {
                var objectsToReturn = new List<string>();

                var fixtureData = specimenBuilder.CreateMany<T>(numOfObjects);

                foreach (var data in fixtureData)
                {
                    objectsToReturn.Add(stringGenerator.Generate(data));
                }

                return objectsToReturn;
            }, token);
        }
    }
}