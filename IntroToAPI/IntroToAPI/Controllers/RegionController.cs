using IntroToAPI.Interface;
using IntroToAPI.ModelResponse;
using IntroToAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntroToAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        public RegionController(IRegionRepository _regionRepository)
        {
            this._regionRepository = _regionRepository; 
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRegion(Region obj)
        {
            try
            {
                var region = await _regionRepository.AddRegion(obj);

                if (region.Success)
                    return Ok(region);

                else
                    return BadRequest(region);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllRegions()
        {
            var region = await _regionRepository.DisplayRegion();

            try
            {
                if(region == null)
                {
                    return NotFound();
                }

                else
                    return Ok(region);
            }

            catch(Exception ex)
            {
                return StatusCode(500, new RegionModelResponse { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("code")]
        public async Task<IActionResult> DisplayRegionByCode(string code)
        {
            var region = await _regionRepository.RegionByCode(code);

            try
            {
                if (region == null)
                    return NotFound();

                else
                    return Ok(region);
            }

            catch(Exception ex)
            {
                return BadRequest(new RegionModelResponse { Success = false, Message = ex.Message});
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRegionDetail(Region obj)
        {
            try
            {
                var updateRegion = await _regionRepository.ModifyRegionDetails(obj);    

                if(updateRegion == null)
                {
                    return NotFound("Code does not exist");
                }

                return Ok(updateRegion);
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
