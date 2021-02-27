using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA20.P05.Web.Data;
using FA20.P05.Web.Features.TemperatureRecords;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;

namespace FA20.P05.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureRecordDtoesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public TemperatureRecordDtoesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        // GET: api/TemperatureRecordDtoes
        private static Expression<Func<TemperatureRecord, TemperatureRecordDto>> MapToDto()
        {
            // we could do something like Set<School>().Select(...) where the dots would be the expression below,
            // but we can re-use this method in several places and reduce our duplication
            return x => new TemperatureRecordDto
            {
                SchoolId = x.SchoolId,
                temperatureFahrenheit = x.temperatureFahrenheit,
                Id = x.Id,
                Qrcode = x.Qrcode
            };
        }

        [HttpGet]
        public IEnumerable<TemperatureRecordDto> GetAll()
        {
            return dataContext
                .Set<TemperatureRecord>()
                .Where(x => x.temperatureFahrenheit>0)
                .Select(MapToDto());
        }

        // GET: api/TemperatureRecordDtoes/5
[HttpGet("active")]
        public IEnumerable<TemperatureRecordDto> GetAllActive()
        {
            return dataContext
                .Set<TemperatureRecord>()
                .Where(x => x.temperatureFahrenheit>99)
                .Select(MapToDto());
        }

        // GET: api/TemperatureRecordDtoes/5

    }
}
