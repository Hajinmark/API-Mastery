using IntroToAPI.Data;
using IntroToAPI.ModelResponse;
using IntroToAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntroToAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionsController(NZWalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = _dbContext.Regions.ToList();

            if(regions == null)
            {
                return NotFound();
            }

            return Ok(regions);
        }

        [HttpGet("Code")]
        public IActionResult GetRegionByCode(string Code)
        {
            var regionCode = _dbContext.Regions.Where(x => x.Code == Code).FirstOrDefault();

            if(regionCode == null)
            {
                return NotFound();  
            }

            return Ok(regionCode);  
        }

        [HttpPost]
        public IActionResult CreateNewRegion(Region obj)
        {
            var isRegionExist = _dbContext.Regions.Where(x => x.Code == obj.Code).FirstOrDefault();
            
            if(isRegionExist == null)
            {
                var region = new Region
                {
                    Code = obj.Code,
                    Name = obj.Name,
                    RegionImageUrl = obj.RegionImageUrl,    
                };

                _dbContext.Regions.Add(region);
                _dbContext.SaveChanges();

                var response = new RegionModelResponse {Success = true, Message = "Successfully Added New Region" };
                return Ok(response);

            }

            else
            {
                var response = new RegionModelResponse { Success = false, Message = "Failed to add new region" };
                return BadRequest(response);
            }

        }

        [HttpPut("Code")]
        public IActionResult UpdateRegion(Guid Id, Region obj)
        {
            var isIdExist = _dbContext.Regions.Where(x => x.Id == Id).FirstOrDefault();

            if(isIdExist != null)
            {

                isIdExist.Name = obj.Name;
                isIdExist.Code = obj.Code;
                isIdExist.RegionImageUrl = obj.RegionImageUrl;

                _dbContext.Regions.Update(isIdExist);
                _dbContext.SaveChanges();

                var response = new RegionModelResponse { Success = true, Message = "Successfully Updated"};
                return Ok(response);

            }

            else
            {
                var response = new RegionModelResponse { Success = false, Message = "The Id you entered does not exist" };
                return BadRequest(response);
            }
            
        }

        [HttpDelete("Id")]
        public IActionResult DeleteRegionById(Guid Id)
        {
            var isRegionIdExist = _dbContext.Regions.Where(x => x.Id == Id).FirstOrDefault();

            if(isRegionIdExist != null)
            {
                _dbContext.Regions.Remove(isRegionIdExist);
                _dbContext.SaveChanges();

                return Ok(isRegionIdExist);
            }

            return NotFound();

        }

        //// Hard Coded 
        //[HttpGet]
        //public IActionResult GetAllRegions()
        //{
        //    var regions = new List<Region>
        //    {
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Kanto Region",
        //            Code = "KR",
        //            RegionImageUrl = "SampleURLOnly"
        //        },

        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Kalon Region",
        //            Code = "KLR",
        //            RegionImageUrl = "Hakdog"
        //        }
        //    };

        //    return Ok(regions);
        //}
    }
}
