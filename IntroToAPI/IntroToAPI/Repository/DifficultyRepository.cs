using System.Linq.Expressions;
using IntroToAPI.Data;
using IntroToAPI.Interface;
using IntroToAPI.ModelResponse;
using IntroToAPI.Models.Domain;
using IntroToAPI.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace IntroToAPI.Repository
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public DifficultyRepository(NZWalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<Difficulty> AddDifficulty(Difficulty obj)
        {
            try
            {
                var isDifficultyExist = await _dbContext.Difficulties.Where(x => x.Name == obj.Name).FirstOrDefaultAsync();
                    
                if(isDifficultyExist == null)
                {
                    var addDifficulty = new Difficulty()
                    {
                        Id = Guid.NewGuid(),
                        Name = obj.Name
                    };

                    await _dbContext.AddAsync(addDifficulty);
                    await _dbContext.SaveChangesAsync();

                    return addDifficulty;
                }

                return null;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RegionModelResponse> AddWalk(WalkViewModel obj)
        {
            try
            {

                var region = new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = obj.Code,
                    Name = obj.Name,
                    RegionImageUrl = obj.RegionImageUrl
                };

                await _dbContext.Regions.AddAsync(region);
                await _dbContext.SaveChangesAsync();

                var difficulty = new Difficulty()
                {
                    Id = Guid.NewGuid(),
                    Name = obj.Name,
                };

                await _dbContext.Difficulties.AddAsync(difficulty);
                await _dbContext.SaveChangesAsync();

                var walk = new Walk()
                {
                    Id = Guid.NewGuid() ,
                    Name = obj.Name,
                    Description = obj.Description,
                    LengthInKm  = obj.LengthInKm,
                    WalkImageUrl  = obj.WalkImageUrl,
                    RegionId = region.Id,
                    DifficultyId = difficulty.Id
                };

                await _dbContext.Walks.AddAsync(walk);
                await _dbContext.SaveChangesAsync();

                return new RegionModelResponse() { Success = true, Message = "Inserted"};

            }

            catch(Exception ex)
            {
                return new RegionModelResponse() { Success = false, Message = ex.Message };
            }
               
        }

        public async Task<List<Difficulty>> FilterDifficulty(string name)
        {
            try
            {
                var difficult = _dbContext.Difficulties.AsQueryable();

                if(string.IsNullOrWhiteSpace(name) == false)
                {
                    difficult = difficult.Where(x=>x.Name.Contains(name));
                    return await difficult.ToListAsync();
                }

                return null;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Difficulty>> ListDifficulty()
        {
            var difficulty = await _dbContext.Difficulties.ToListAsync();   
            return difficulty;
        }

        public async Task<List<Difficulty>> SortDifficulty(string? sortyBy, bool isAscending)
        {
            try
            {
                sortyBy = "Name";
                isAscending = true;

                var difficult = _dbContext.Difficulties.AsQueryable();

                if(string.IsNullOrEmpty(sortyBy) == false)
                {
                    if(sortyBy.Equals("Name"))
                    {
                        difficult = isAscending ? difficult.OrderBy(x => x.Name) : difficult.OrderByDescending(x => x.Name);    
                        return await difficult.ToListAsync();
                    }

                    return null;
                    
                }

                return null;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Difficulty> UpdateDifficulty(Guid id,Difficulty obj)
        {
            try
            {
                var isDifficultyExist = await _dbContext.Difficulties.Where(x => x.Id == id).FirstOrDefaultAsync();
                
                if(isDifficultyExist != null)
                {
                    isDifficultyExist.Name = obj.Name;

                    _dbContext.Difficulties.Update(isDifficultyExist);
                    await _dbContext.SaveChangesAsync();    
                    
                    return isDifficultyExist;
                }

                return null;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
