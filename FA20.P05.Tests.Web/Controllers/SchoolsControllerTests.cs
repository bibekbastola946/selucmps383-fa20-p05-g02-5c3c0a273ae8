using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FA20.P05.Tests.Web.Helpers;
using FA20.P05.Web.Features.Schools;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FA20.P05.Tests.Web.Controllers
{
    [TestClass]
    public class SchoolsControllerTests
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
        public async Task GetAllActiveSchools_ReturnsOk()
        {
            //arrange
            var webClient = context.GetStandardWebClient();

            //act
            var httpResponse = await webClient.GetAsync("/api/schools/active");

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK, "we expect get active schools to always work");
            var resultDto = await httpResponse.Content.ReadAsJsonAsync<List<SchoolDto>>();
            resultDto.Should().HaveCountGreaterThan(0, "we expect at least some records from get active schools");
        }
    }
}