using FluentAssertions;
using Sitecore.Globalization;
using Xunit;

namespace Sitecore.FakeDb.Demo
{
    public class LocalizationTest
    {
        [Fact]
        public void GetLocalizedText()
        {
            using (var db = new Db())
            {
                db.Configuration.Settings.AutoTranslate = true;
                db.Configuration.Settings.AutoTranslatePrefix = "{lang}-";

                var language = Language.Parse("da");
                var translatedText = Translate.TextByLanguage("hello", language);
                translatedText.Should().Be("da-hello");
            }
        }
    }
}
