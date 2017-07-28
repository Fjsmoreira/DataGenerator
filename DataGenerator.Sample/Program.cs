using System;
using System.IO;
using Ploeh.AutoFixture;

namespace DataGenerator.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringGenerator = new ObjectToStringGenerator(new StringGeneratorOptions { WithPropertyName = true, Separator = ", " });
            var dataGenerator = new RandomDataGenerator<GrossMargin>(new Fixture(), stringGenerator);

            for (var i = 0; i < 10; i++)
            {
                var subscription = dataGenerator.CreateMany(100);
                File.AppendAllLines($@"{Directory.GetCurrentDirectory()}\xpto{i}.txt", subscription);

            }
        }
        public class GrossMargin
        {
            public string Metric { get; set; }
            public TimeSpan Timespan { get; set; }
            public decimal Value { get; set; }
            public string Brand { get; set; }

        }
    }
}
