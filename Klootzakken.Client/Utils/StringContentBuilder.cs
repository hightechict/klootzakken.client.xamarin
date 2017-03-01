using System.Net.Http;
using System.Collections.Generic;
using System.Text;

namespace Klootzakken.Client.Utils
{
    public class StringContentBuilder
    {
        private const string COMA_SEPARATOR = ",";

        private Encoding _charSet;
        private string _mediaType;

        public StringContentBuilder(Encoding charSet, string mediaType)
        {
            _charSet = charSet;
            _mediaType = mediaType;
        }

        public StringContent build(params System.Collections.Generic.KeyValuePair<string, string>[] keyValuePairs)
        {
            return new StringContent(buildContent(keyValuePairs),
                         _charSet,
                         _mediaType);
        }

        private static string buildContent(KeyValuePair<string, string>[] keyValuePairs)
        {
            string content = "{";
            content = buildContentWithValuesOfKeyValuePairs(keyValuePairs, content);
            content += "}";

            return content;
        }

        private static string buildContentWithValuesOfKeyValuePairs(KeyValuePair<string, string>[] keyValuePairs, string content)
        {
            for (int i = 0; i < keyValuePairs.Length; i++)
            {
                content += getParsedContentOfKeyValuePair(keyValuePairs[i]);
                content += addComaSeparatorIfThereIsMoreKeyValuePair(keyValuePairs, i);
            }

            return content;
        }

        private static string getParsedContentOfKeyValuePair(KeyValuePair<string, string> keyValuePair)
        {
            return "\"" + keyValuePair.Key + "\":\"" + keyValuePair.Value + "\"";
        }

        private static string addComaSeparatorIfThereIsMoreKeyValuePair(KeyValuePair<string, string>[] keyValuePairs, int i)
        {
            return !isLastElement(keyValuePairs, i)
                ? COMA_SEPARATOR
                : "";
        }

        private static bool isLastElement(KeyValuePair<string, string>[] keyValuePairs, int i)
        {
            return i == keyValuePairs.Length - 1;
        }
    }
}