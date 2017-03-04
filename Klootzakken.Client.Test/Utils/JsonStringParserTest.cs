using Klootzakken.Client.Utils;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Klootzakken.Client.Test.Utils
{
    public class JsonStringParserTest
    {

        public string accessTokenJson = "{\"access_token\":\"abcdefghijklmnopqrstuvwxyzs\",\"expires_in\":86400}";

        private string parseJsonObject(string jsonParameterName, string jsonString) => JsonStringParser.GetValue(jsonParameterName, jsonString);

        [Fact]
        public void jsonObjectWithParameter_getValue_returnsSpecifiedJsonValue()
        {
            Assert.Equal("abcdefghijklmnopqrstuvwxyzs", parseJsonObject("access_token", accessTokenJson));
        }

        [Fact]
        public void jsonObjectWithAntoherParameter_getValue_returnsSpecifiedJsonValue()
        {
            Assert.Equal("86400", parseJsonObject("expires_in", accessTokenJson));
        }

    }
}
