using IntroToAPI.Interface;
using IntroToAPI.Models.Domain;
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

        [HttpPost("CreateNewRegion")]
        public async Task<IActionResult> CreateNewRegion(Region obj)
        {
            try
            {
                var region = await _regionRepository.AddRegion(obj);

                if (region.Success)
                {
                    return Ok(region);
                }

                return BadRequest(region);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("AllRegions")]
        public async Task<IActionResult> AllRegions()
        {
            var region = await _regionRepository.DisplayRegion();

            try
            {
                if (region != null)
                {
                    return Ok(region);
                }

                else
                {
                    return BadRequest(region);
                }
            }

            catch (Exception ex)
            {
                return StatusCode(00, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UdateRegions(Region obj)
        {
            try
            {
                var region = await _regionRepository.ModifyRegionDetails(obj);

                if (region != null)
                {
                    return Ok(region);
                }

                else
                {
                    return BadRequest(region);
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRegionByCode(string code)
        {
            var regionCode = await _regionRepository.RegionByCode(code);

            return Ok(regionCode);
        }

        [HttpGet("FilterRegion")]
        public async Task<IActionResult> FilterRegion(string code)
        {
            try
            {
                var region = await _regionRepository.FilterRegion(code);

                if(region != null)
                {
                    return Ok(region);
                }

                return BadRequest(region);  
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("SortRegion")]
        public async Task<IActionResult> SortRegion(string sortBy, bool isAscending)
        {
            try
            {
                var region = await _regionRepository.SortRegion(sortBy, isAscending);

                if(region != null)
                {
                    return Ok(region);
                }

                return BadRequest(region);
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
