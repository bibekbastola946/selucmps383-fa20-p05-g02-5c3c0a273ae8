using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FA20.P05.Web.Data;
using FA20.P05.Web.Features.Authentication;
using FA20.P05.Web.Features.Schools;
using FA20.P05.Web.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA20.P05.Web.Controllers
{
    [ApiController]
    [Route("api/schools")]
    public class SchoolsController : ControllerBase
    {
        // we use dependency injection to grab an instance of DataContext
        private readonly DataContext dataContext;

        public SchoolsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        private static Expression<Func<School, SchoolDto>> MapToDto()
        {
            // we could do something like Set<School>().Select(...) where the dots would be the expression below,
            // but we can re-use this method in several places and reduce our duplication
            return x => new SchoolDto
            {
                Name = x.Name,
                Active = x.Active,
                SchoolPopulation = x.SchoolPopulation,
                Id = x.Id
            };
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<SchoolDto> GetAll()
        {
            var username = User.Identity.Name;
            var isAdmin = User.IsInRole(Roles.Admin);
            return dataContext
                .Set<School>()
                .Where(x => isAdmin || x.Staff.Any(y => y.Staff.Users.Any(z => z.UserName == username)))
                .Select(MapToDto());
        }

        [HttpGet("active")]
        public IEnumerable<SchoolDto> GetAllActive()
        {
            return dataContext
                .Set<School>()
                .Where(x => x.Active)
                .Select(MapToDto());
        }

        [HttpGet("{id}")]
        public ActionResult<SchoolDto> GetById(int id)
        {
            var isAdmin = User.IsInRole(Roles.Admin);
            var data = dataContext
                .Set<School>()
                .Where(x => x.Id == id && (isAdmin || x.Active))
                .Select(MapToDto())
                .FirstOrDefault();
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult<SchoolDto> Create(SchoolDto targetValue)
        {
            if (string.IsNullOrWhiteSpace(targetValue.Name))
            {
                return BadRequest();
            }
            // TODO: there are other rules for validation to consider
            // e.g. we have a limit to the school name length as well as we should ensure that zip code is in the right format
            var result = dataContext.Set<School>().Add(new School
            {
                Name = targetValue.Name,
                Active = targetValue.Active,
                SchoolPopulation = targetValue.SchoolPopulation,
                Address = targetValue.Address == null
                    ? null
                    : new Address
                    {
                        AddressLine1 = targetValue.Address.AddressLine1,
                        AddressLine2 = targetValue.Address.AddressLine2,
                        City = targetValue.Address.City,
                        State = targetValue.Address.State,
                        Zip = targetValue.Address.Zip
                    }
            });
            dataContext.SaveChanges();
            targetValue.Id = result.Entity.Id;
            return Created($"/api/schools/{targetValue.Id}", targetValue);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.PrincipalPlus)]
        public ActionResult<SchoolDto> Update(int id, SchoolDto targetValue)
        {
            var username = User.Identity.Name;
            var isAdmin = User.IsInRole(Roles.Admin);

            var data = dataContext.Set<School>()
                .Where(x => isAdmin || x.Staff.Any(y => y.Staff.Users.Any(z => z.UserName == username)))
                .FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            // notice the duplication here and in create
            // maybe we can reduce that duplication somehow?
            data.Name = targetValue.Name;
            data.Active = targetValue.Active;
            data.SchoolPopulation = targetValue.SchoolPopulation;
            data.Address = targetValue.Address == null
                ? null
                : new Address
                {
                    AddressLine1 = targetValue.Address.AddressLine1,
                    AddressLine2 = targetValue.Address.AddressLine2,
                    City = targetValue.Address.City,
                    State = targetValue.Address.State,
                    Zip = targetValue.Address.Zip
                };

            dataContext.SaveChanges();

            return Ok(targetValue);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Delete(int id)
        {
            var data = dataContext.Set<School>().FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            dataContext.Set<School>().Remove(data);
            dataContext.SaveChanges();

            return Ok();
        }
    }
}
