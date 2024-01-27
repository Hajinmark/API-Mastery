using IntroToAPI.Data;
using IntroToAPI.Interface;
using IntroToAPI.Models.Domain;
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
