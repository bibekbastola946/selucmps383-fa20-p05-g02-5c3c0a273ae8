using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FA20.P05.Web.Data;
using FA20.P05.Web.Features.Authentication;
using FA20.P05.Web.Features.Schools;
using FA20.P05.Web.Features.SchoolStaffMembers;
using FA20.P05.Web.Features.StaffMembers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA20.P05.Web.Controllers
{
    // In this controller I'm going to use async + await
    // see: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async
    // currently, microsoft suggests using async versions of any method that would be IO / Network bound
    // that is mostly so that server can handle more requests per second overall
    [ApiController]
    [Route("api/staff")]
    public class StaffController : ControllerBase
    {
        private readonly DataContext dataContext;

        public StaffController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // This same rule is used several times - so you can extract it to a common method to reduce code duplication
        private Expression<Func<Staff, bool>> StaffVisibleToRequest()
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return x => false;
            }

            var username = User.Identity.Name;
            var isAdmin = User.IsInRole(Roles.Admin);
            return x => isAdmin ||
                        // the logic here might be tricky
                        // since a staff person can be in several schools
                        // we will look at all school staff members at all schools which this staff member belongs
                        // if any of those records have the requesting user at the same school then it is valid to show
                        x.Schools
                            .SelectMany(y => y.School.Staff)
                            .SelectMany(y => y.Staff.Users)
                            .Any(y => y.UserName == username);
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<StaffDto>> GetAll()
        {
            // another way of getting data
            // this is almost the same as the GET api/schools
            // the main difference is the Task<> and asynchronous nature of the data fetch
            // see: https://docs.microsoft.com/en-us/ef/core/querying/async
            return await dataContext
                .Set<Staff>()
                .Where(StaffVisibleToRequest())
                .Select(x => new StaffDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<StaffDto>> GetById(int id)
        {
            var result = await dataContext
                .Set<Staff>()
                .Where(StaffVisibleToRequest())
                .Select(x => new StaffDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        [Authorize(Roles = Roles.PrincipalPlus)]
        public async Task<ActionResult<EditStaffDto>> Create(CreateStaffDto targetValue)
        {
            if (string.IsNullOrWhiteSpace(targetValue.LastName) || string.IsNullOrWhiteSpace(targetValue.FirstName))
            {
                return BadRequest();
            }

            var username = User.Identity.Name;
            var isAdmin = User.IsInRole(Roles.Admin);

            var data = dataContext.Set<School>()
                .Where(x => targetValue.Schools.Contains(x.Id))
                .All(x => isAdmin ||
                          // the principal requesting must be among the staff at the school being requested
                          x.Staff
                              .SelectMany(y => y.Staff.Users)
                              .Any(z => z.UserName == username));
            if (!data)
            {
                return BadRequest();
            }

            // this is an example of different DTOs serving different purposes
            // e.g. create does not specify the ID where edit staff should have the ID
            var result = dataContext.Set<Staff>().Add(new Staff
            {
                FirstName = targetValue.FirstName,
                LastName = targetValue.LastName,
                CreatedUtc = DateTimeOffset.UtcNow,
                // notice the '?.' symbol, that is the null conditional operator
                // see: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-
                // this will prevent an exception from being thrown if thar targetValue.Schools is null

                // this is also a random example of creating several entities at once using EF core
                // in this case we are effectively assigning a staff person to whatever school Ids are passed in
                Schools = targetValue.Schools?.Select(x => new SchoolStaff
                {
                    SchoolId = x
                }).ToList()
            });

            await dataContext.SaveChangesAsync();
            var savedEntity = result.Entity;
            return Created($"/api/schools/{savedEntity.Id}", new EditStaffDto
            {
                FirstName = savedEntity.FirstName,
                LastName = savedEntity.LastName,
                Id = savedEntity.Id
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.PrincipalPlus)]
        public async Task<ActionResult<SchoolDto>> Update(int id, EditStaffDto targetValue)
        {
            var entity = await dataContext
                .Set<Staff>()
                .Where(StaffVisibleToRequest())
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(targetValue.LastName) || string.IsNullOrWhiteSpace(targetValue.FirstName))
            {
                return BadRequest();
            }

            entity.FirstName = targetValue.FirstName;
            entity.LastName = targetValue.LastName;

            await dataContext.SaveChangesAsync();
            targetValue.Id = entity.Id;

            return Ok(targetValue);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await dataContext.Set<Staff>().FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            dataContext.Set<Staff>().Remove(data);
            await dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
