using System.IO;
using FluentAssertions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Xunit;

namespace Sitecore.FakeDb.Demo
{
    public class BlobTests
    {
        [Fact]
        public void GetDataFromBlob()
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream) { AutoFlush = true };
            writer.Write("hello from blob");

            using (var db = new Db { new DbItem("media")
            {
                new DbField("data")
            } })
            {
                var item = Database.GetDatabase("master")
                    .GetItem("/sitecore/content/media");
                var field = item.Fields["data"];
                using (new EditContext(item))
                {
                    field.SetBlobStream(memoryStream);
                }

                var resultStream = field.GetBlobStream();
                var reader = new StreamReader(resultStream);
                reader.ReadToEnd().Should().Be("hello from blob");
            }
        }
    }
}
