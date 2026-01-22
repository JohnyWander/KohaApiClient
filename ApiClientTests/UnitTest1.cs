using AllcandoJM.KohaFramework.ApiCore;
using Moq;

namespace AllcandoJM.KohaFramework.ApiTests
{
    public class Tests
    {
        private Mock<ApiCore.ApiClient> _apiMock;
        private ApiCore.ApiClient _client;

        private Resources _res = new Resources();

        [SetUp]
        public void Setup()
        {
            _apiMock = new Mock<ApiCore.ApiClient>();
            
            _client = _apiMock.Object;
        }

        [Test]
        public async Task ErrorHandlingTest()
        {
            ApiResponse resp = await _client.ParseResponseAsync(_res.errorResponse());
            Assert.That(resp.IsSuccess == false);
            Assert.Multiple(() =>
            {
                Assert.That(resp.IsSuccess == false);
                Assert.That(resp.ResponseCode == _res.errorResponse().StatusCode);
            });
        }
    }
}