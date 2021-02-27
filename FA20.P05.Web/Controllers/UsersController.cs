using System.Threading.Tasks;
using FA20.P05.Web.Data;
using FA20.P05.Web.Features.Authentication;
using FA20.P05.Web.Features.StaffMembers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA20.P05.Web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly DataContext dataContext;

        public UsersController(
            UserManager<User> userManager,
            DataContext dataContext)
        {
            this.userManager = userManager;
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
        {
            // wrapping in a transaction means that if part of the transaction fails then everything saved is undone
            await using var transaction = await dataContext.Database.BeginTransactionAsync();

            if (dto.Role == Roles.Admin && dto.StaffId.HasValue ||
                dto.Role != Roles.Admin && !dto.StaffId.HasValue)
            {
                return BadRequest();
            }

            if (dto.StaffId.HasValue && !await dataContext.Set<Staff>().AnyAsync(x => x.Id == dto.StaffId))
            {
                return BadRequest();
            }

            if (!await dataContext.Roles.AnyAsync(x => x.Name == dto.Role))
            {
                return BadRequest();
            }

            var newUser = new User
            {
                UserName = dto.Username,
                StaffId = dto.StaffId
            };

            var identityResult = await userManager.CreateAsync(newUser, dto.Password);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            var roleResult = await userManager.AddToRoleAsync(newUser, dto.Role);
            if (!roleResult.Succeeded)
            {
                return BadRequest();
            }

            await transaction.CommitAsync(); // this marks our work as done

            return Created(string.Empty, new UserDto
            {
                Username = newUser.UserName,
                StaffId = newUser.StaffId
            });
        }
    }
}