using FluentAssertions;
using Sitecore.Configuration;
using Xunit;

namespace Sitecore.FakeDb.Demo
{
    public class SettingsTests
    {
        [Fact]
        public void GetSettingFromConfig()
        {
            Settings.GetSetting("setting.from.config").Should().Be("12345");
        }

        [Fact]
        public void GetSettingFromMemory()
        {
            using (var db = new Db())
            {
                db.Configuration.Settings["setting.from.memory"] = "54321";
                Settings.GetSetting("setting.from.memory").Should().Be("54321");
            }
        }
    }
}
