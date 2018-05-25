using NSubstitute;
using Sitecore.FakeDb.Pipelines;
using Sitecore.Pipelines;
using Xunit;

namespace Sitecore.FakeDb.Demo
{
    public class PipelineTests
    {
        [Fact]
        public void EnsurePipelineExecuted()
        {
            using (var db = new Db())
            {
                var arg = new PipelineArgs();
                var processor = Substitute.For<IPipelineProcessor>();
                db.PipelineWatcher.Register("demo", processor);

                CorePipeline.Run("demo", arg);

                processor.Received().Process(arg);
            }
        }
    }
}
