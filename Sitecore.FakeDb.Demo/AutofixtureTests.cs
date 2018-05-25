using FluentAssertions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb.AutoFixture;
using Xunit;

namespace Sitecore.FakeDb.Demo
{
    public class AutofixtureTests
    {
        [Theory, AutoDb]
        public void GetDatabase(Db fakeDb, Database db)
        {
            fakeDb.Should().NotBeNull();
            db.Should().NotBeNull();
        }

        [Theory, AutoDb]
        public void GetItem([Content]DbItem item,  Database db)
        {
            var resultItem = db.GetItem(item.ID);
            resultItem.Should().NotBeNull();
        }

        [Theory, AutoDb]
        public void CreateItemBasedOnCustomTemplate([Content]Item root, [Content]MyCustomTemplate customTemplate)
        {
            var item = root.Add("home", new TemplateID(customTemplate.ID));
            using (new EditContext(item))
            {
                item["Field"] = "TestValue";
            }

            item["Field"].Should().Be("TestValue");
        }

        public class MyCustomTemplate : DbTemplate
        {
            public MyCustomTemplate()
            {
                Add("Field");
            }
        }

        public class AutoDbAttribute : AutoDataAttribute
        {
            public AutoDbAttribute()
                : base(new Fixture().Customize(new AutoDbCustomization()))
            {

            }
        }
    }
}
