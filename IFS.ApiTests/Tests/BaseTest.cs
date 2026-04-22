using IFS.ApiTests.Clients;

namespace IFS.ApiTests.Tests
{
    public class BaseTest
    {
        protected ApiClient Client { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Client = new ApiClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
        }
    }
}