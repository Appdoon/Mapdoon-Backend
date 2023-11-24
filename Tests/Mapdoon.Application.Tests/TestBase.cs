namespace Mapdoon.Application.Tests
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await ResetStateAsync();
            ResetDatabaseContext();
        }
    }
}
