using FluentAssertions;
using Sitecore.Data;
using Sitecore.Globalization;
using Xunit;

namespace Sitecore.FakeDb.Demo
{
    public class ContentTests
    {
        [Fact]
        public void GetsItemFromDatabase()
        {
            using (new Db { new DbItem("home") { { "text", "hello world" } } })
            {
                var db = Database.GetDatabase("master");
                var item = db.GetItem("/sitecore/content/home");
                item["text"].Should().Be("hello world");
            }
        }

        [Fact]
        public void GetChildItemFromDatabase()
        {
            using (new Db
            {
                new DbItem("articles")
                {
                    new DbItem("Article")
                }
            })
            {
                var db = Database.GetDatabase("master");
                var item = db.GetItem("/sitecore/content/articles");
                var childItem = item.Children["Article"];
                childItem.Should().NotBeNull();
            }
        }

        [Fact]
        public void GetItemBasedOnTemplate()
        {
            var templateId = ID.NewID;
            using (new Db
            {
                new DbTemplate("product", templateId) {{"title", "$name"}},
                new DbItem("banana"){TemplateID = templateId}
            })
            {
                var db = Database.GetDatabase("master");
                var item = db.GetItem("/sitecore/content/banana");
                item["title"].Should().Be("banana");
            }
        }

        [Fact]
        public void GetItemVersionFromDatabase()
        {
            using (new Db
            {
                new DbItem("article")
                {
                    new DbField("text")
                    {
                        {"en", 1, "hello"},
                        {"en", 2, "greetings"}
                    }
                }
            })
            {
                var db = Database.GetDatabase("master");
                var lang = Language.Parse("en");
                var ver = Version.Parse(2);
                var item = db.GetItem("/sitecore/content/article", lang, ver);
                item["text"].Should().Be("greetings");
            }
        }
    }
}
