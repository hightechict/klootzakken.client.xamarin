using Klootzakken.Client.Utils;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Klootzakken.Client.Tests
{
    public class StringContentBuilderTest
    {
        private StringContentBuilder stringContentBuilder = new StringContentBuilder(Encoding.UTF8, "application/json");



        [Fact]
        public void nullParameter_createStringContent_returnEmptyContent()
        {
            var stringContent = stringContentBuilder.build();

            Assert.Equal(stringContent.Headers.ContentType.CharSet, "utf-8");
            Assert.Equal(stringContent.Headers.ContentType.MediaType, "application/json");
            Assert.Equal(stringContent.ReadAsStringAsync().Result, "{}");
        }


        [Fact]
        public void OneKeyValueParameter_CreateStringContent_StringContentIsBuiltUp () {
            var stringContent = stringContentBuilder.build(KeyValuePair.Create("name", "DanielsLobby"));

            Assert.Equal(stringContent.Headers.ContentType.CharSet, "utf-8");
            Assert.Equal(stringContent.Headers.ContentType.MediaType, "application/json");
            Assert.Equal(stringContent.ReadAsStringAsync().Result, "{\"name\":\"DanielsLobby\"}");
        }



        [Fact]
        public void MultipleKeyValueParameters_CreateStringContent_StringContentIsBuiltUp()
        {
            var stringContent = stringContentBuilder.build(
                KeyValuePair.Create("name", "DanielsLobby"),
                KeyValuePair.Create("id", "DanielsId"));

            Assert.Equal(stringContent.Headers.ContentType.CharSet, "utf-8");
            Assert.Equal(stringContent.Headers.ContentType.MediaType, "application/json");
            Assert.Equal(stringContent.ReadAsStringAsync().Result, "{\"name\":\"DanielsLobby\",\"id\":\"DanielsId\"}");
        }
    }
}