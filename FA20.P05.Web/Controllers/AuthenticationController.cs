using System.Linq;
using System.Threading.Tasks;
using FA20.P05.Web.Data;
using FA20.P05.Web.Features.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA20.P05.Web.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DataContext dataContext;

        public AuthenticationController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            DataContext dataContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dataContext = dataContext;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<UserDto> Me()
        {
            var userName = User.Identity.Name;
            return await dataContext.Set<User>()
                .Where(x => x.UserName == userName)
                .Select(x => new UserDto
                {
                    StaffId = x.StaffId,
                    Username = x.UserName
                })
                .FirstOrDefaultAsync();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.Username);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, true);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            await signInManager.SignInAsync(user, false, "Password");
            return Ok(new UserDto
            {
                Username = user.UserName,
                StaffId = user.StaffId
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}