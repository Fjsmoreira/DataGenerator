using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnsureThat;

namespace DataGenerator
{
    public class ObjectToStringGenerator : IStringGenerator
    {
        private string DefaultSeparator = ",";
        public StringGeneratorOptions Options { get; }
        public ObjectToStringGenerator(StringGeneratorOptions options)
        {
            EnsureArg.IsNotNull(options, nameof(options));
            Options = options;
            DefaultOptions();
        }

        private void DefaultOptions()
        {
            if (string.IsNullOrEmpty(Options.Separator))
            {
                Options.Separator = DefaultSeparator;
            }
        }

        public string Generate(object fromObject)
        {
            return string.Join(Options.Separator, ValuesToString(fromObject));
        }

        private IEnumerable<string> ValuesToString(object data)
        {
            var properties = data.GetType().GetRuntimeProperties();

            if (Options.WithPropertyName)
            {
                return properties.Select(_ => $"{_.Name} = {_.GetValue(data)}");
            }

            return properties.Select(_ => $"{_.GetValue(data)}");
        }

    }
}