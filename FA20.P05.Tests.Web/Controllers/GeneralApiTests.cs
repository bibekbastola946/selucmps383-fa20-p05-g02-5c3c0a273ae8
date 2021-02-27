using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FA20.P05.Tests.Web.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FA20.P05.Tests.Web.Controllers
{
    [TestClass]
    public class GeneralApiTests
    {
        private WebTestContext context;

        [TestInitialize]
        public void Init()
        {
            context = new WebTestContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }

        [TestMethod]
        public async Task EnsureOnlyDtosAreUsedInApi()
        {
            var webClient = context.GetStandardWebClient();
            var httpResponse = await webClient.GetAsync("/swagger/v1/swagger.json");
            var apiSpec = await httpResponse.Content.ReadAsJsonAsync<OpenApiSpec>();

            var violations = apiSpec.Components.Schemas.Where(x => !x.Key.EndsWith("Dto")).Select(x => x.Key).ToList();
            Assert.IsTrue(violations.Count == 0, $"You have entities being sent in your API:\r\n{string.Join("\r\n", violations)}");
        }

        [TestMethod]
        public async Task EnsureSwaggerIsInstalled()
        {
            // we assume they will follow the default
            var webClient = context.GetStandardWebClient();
            var httpResponse = await webClient.GetAsync("/swagger/v1/swagger.json");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var httpResponse2 = await webClient.GetAsync("/swagger");
            httpResponse2.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
