using IntroToAPI.Interface;
using IntroToAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IntroToAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository _difficultyRepository;
        public DifficultyController(IDifficultyRepository _difficultyRepository)
        {
            this._difficultyRepository = _difficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult>AllDifficulty()
        { 
            try
            {
                var difficult = await _difficultyRepository.ListDifficulty();

                if(difficult != null)
                {
                    return Ok(difficult);
                }

                return BadRequest("Empty Data");
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult>AddNewDifficulty(Difficulty obj)
        {
            try
            {
                var difficulty = await _difficultyRepository.AddDifficulty(obj);

                if(difficulty != null)
                {
                    return Ok(difficulty);
                }

                return BadRequest(difficulty);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateDifficulty(Guid id, Difficulty obj)
        {
            try
            {
                var update = await _difficultyRepository.UpdateDifficulty(id,obj);

                if(update != null)
                {
                    return Ok(update);   
                }

                return BadRequest(obj);
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }

        [HttpGet("FilterDifficulty")]
        public async Task<IActionResult> FilterByDifficulty(string name)
        {
            try
            {
                var difficult = await _difficultyRepository.FilterDifficulty(name); 

                if(difficult != null)
                {
                    return Ok(difficult);
                }

                return BadRequest(difficult);
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("SortDifficulty")]
        public async Task<IActionResult> SortByDifficulty(string name, bool isAscending)
        {
            try
            {
                var difficulty = await _difficultyRepository.SortDifficulty(name, isAscending);

                if(difficulty != null)
                {
                    return Ok(difficulty);
                }

                return BadRequest(difficulty);

            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
