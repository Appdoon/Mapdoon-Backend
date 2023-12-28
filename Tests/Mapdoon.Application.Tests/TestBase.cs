namespace Mapdoon.Application.Tests
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            Assert.Pass();
            //await ResetStateAsync();
            //ResetDatabaseContext();
            //ResetFacadeFileHandler();
        }
    }
}
