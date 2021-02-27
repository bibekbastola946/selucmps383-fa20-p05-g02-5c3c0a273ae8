using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FA20.P05.Web.Data;
using FA20.P05.Web.Features.Authentication;
using FA20.P05.Web.Features.TemperatureRecords;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using QRCoder;

namespace FA20.P05.Web.Controllers
{
    [ApiController]
    [Route("api/temperature-records")]
    public class TemperatureRecordsController : ControllerBase  
    {
        private readonly DataContext dataContext;
        private readonly IWebHostEnvironment _env;
        public TemperatureRecordsController(DataContext dataContext, IWebHostEnvironment env)
        {
            this.dataContext = dataContext;
            _env = env;
        }
        private static Expression<Func<TemperatureRecord, TemperatureRecordDto>> MapToDto()
        {
            // we could do something like Set<School>().Select(...) where the dots would be the expression below,
            // but we can re-use this method in several places and reduce our duplication
            return x => new TemperatureRecordDto
            {
                SchoolId = x.SchoolId,
                temperatureFahrenheit = x.temperatureFahrenheit,
                Id = x.Id
            };
        }
        private string GetImagePath()
    => ($"{_env.ContentRootPath}\\uploads\\{DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".jpg"}");

        [HttpPost]
        [Authorize(Roles = Roles.PrincipalStaff)]

        public async Task<ActionResult<TemperatureRecordDto>> Create(TemperatureRecordCreateDto targetValue)
        {
            var username = User.Identity.Name;

            // don't allow a staff to specify a school they are not a staff member at
            var schoolStaff = await dataContext.Set<User>()
                .Where(x => x.UserName == username)
                .SelectMany(x => x.Staff.Schools)
                .Where(x => x.SchoolId == targetValue.SchoolId)
                .FirstOrDefaultAsync();

            if (schoolStaff == null)
            {
                return NotFound();
            }

            // TODO: what validation rules might you want to add here?
            var result = dataContext.Set<TemperatureRecord>().Add(new TemperatureRecord
            {     
                SchoolId = schoolStaff.SchoolId,
                StaffId = schoolStaff.StaffId,
                temperatureFahrenheit = targetValue.temperatureFahrenheit,
                MeasuredUtc = DateTimeOffset.UtcNow
            }); ;
           
            await dataContext.SaveChangesAsync();
            var qrImage = GenerateQRImage(Convert.ToString(DateTimeOffset.UtcNow), Convert.ToString(result.Entity.Id));
            result.Entity.Qrcode = qrImage;
            await dataContext.SaveChangesAsync();
            //targetValue.Id = result.Entity.Id;

            //hmm, maybe we need more endpoints later?
            return Created($"/api/temperature-records/{result.Entity.Id}", targetValue);
        }

      /*  [HttpGet]
      
        public async Task<IEnumerable<TemperatureRecord>> GetAll()
        {
            // another way of getting data
            // this is almost the same as the GET api/schools
            // the main difference is the Task<> and asynchronous nature of the data fetch
            // see: https://docs.microsoft.com/en-us/ef/core/querying/async
            return await dataContext
                .Set<TemperatureRecord>()  
                .ToListAsync();
        //}
        */

        [HttpGet("{id}")]
        public IEnumerable<TemperatureRecordDto> GetById(int id)
        {
            return dataContext
                .Set<TemperatureRecord>()
                .Where(x => x.Id== id)
                .Select(MapToDto());
        }

        private string GenerateQRImage(string date,string qrText)
        {
            byte[] imageByte;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                imageByte = stream.ToArray();
              
               

            }
            var baseimage = "data:image/png;base64," + Convert.ToBase64String(imageByte);
            var imgPath = GetImagePath();
            string cleandata = baseimage.Replace("data:image/png;base64,", "");
            byte[] data = System.Convert.FromBase64String(cleandata);
            MemoryStream ms = new MemoryStream(data);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            return baseimage;
        }
    }
}