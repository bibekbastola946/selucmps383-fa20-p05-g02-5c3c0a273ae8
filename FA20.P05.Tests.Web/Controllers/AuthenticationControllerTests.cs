using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FA20.P05.Tests.Web.Helpers;
using FA20.P05.Web.Features.Authentication;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FA20.P05.Tests.Web.Controllers
{
    [TestClass]
    public class AuthenticationControllerTests
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
        public async Task Login_WithValidAdmin_ReturnsOk()
        {
            //arrange
            var webClient = context.GetStandardWebClient();

            //act
            await LoginTo(webClient, admin);

            //assert
            await Logout(webClient);
        }

        [TestMethod]
        public async Task Login_WithValidPrincipal_ReturnsOk()
        {
            //arrange
            var webClient = context.GetStandardWebClient();

            //act
            await LoginTo(webClient, principal);

            //assert
            await Logout(webClient);
        }

        [TestMethod]
        public async Task Login_WithValidStaff_ReturnsOk()
        {
            //arrange
            var webClient = context.GetStandardWebClient();

            //act
            await LoginTo(webClient, staff);

            //assert
            await Logout(webClient);
        }

        [TestMethod]
        public async Task Login_WithNothing_ReturnsBadRequest()
        {
            //arrange
            var webClient = context.GetStandardWebClient();

            //act
            var response = await webClient.PostAsJsonAsync("/api/authentication/login", anon);

            //assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Expected bad request when specifying no username / password while logging in");
        }

        private static async Task Logout(HttpClient webClient)
        {
            var response = await webClient.PostAsync("/api/authentication/logout", null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Expected to be able to logout after logging in");
        }

        private static async Task LoginTo(HttpClient webClient, LoginDto loginDto)
        {
            var response = await webClient.PostAsJsonAsync("/api/authentication/login", loginDto);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Expected to be able to login as '" + loginDto.Username.ToLowerInvariant() + "' with password: " + loginDto.Password);
            var parsed = await response.Content.ReadAsJsonAsync<UserDto>();
            Assert.IsTrue(string.Equals(loginDto.Username, parsed?.Username, StringComparison.InvariantCultureIgnoreCase), "Expected data to be returned after login, and for that data to at least include the username");

            var meEndpoint = await webClient.GetAsync("/api/authentication/me");
            Assert.AreEqual(HttpStatusCode.OK, meEndpoint.StatusCode, "Expected to GET /api/authentication/me after logging in");
            var meData = await meEndpoint.Content.ReadAsJsonAsync<UserDto>();
            meData.Should().BeEquivalentTo(new { parsed?.Username }, "The result from login should be the same values as what the user gets from hitting the 'me' endpoint");

            if (loginDto == principal || loginDto == staff)
            {
                Assert.IsNotNull(meData.StaffId, "Expected the result of GET /api/authentication/me to have a staffId for principals and staff");
            }
        }

        private static string passwordForEveryone = "Password123!";
        private static LoginDto admin = new LoginDto
        {
            Username = "AdmIn",
            Password = passwordForEveryone
        };

        private static LoginDto principal = new LoginDto
        {
            Username = "PrIncipal",
            Password = passwordForEveryone
        };

        private static LoginDto staff = new LoginDto
        {
            Username = "StAff",
            Password = passwordForEveryone
        };

        private static LoginDto anon = new LoginDto
        {
            Username = string.Empty,
            Password = string.Empty
        };
    }
}
