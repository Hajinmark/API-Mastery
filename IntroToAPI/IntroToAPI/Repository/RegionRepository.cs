using IntroToAPI.Data;
using IntroToAPI.Interface;
using IntroToAPI.ModelResponse;
using IntroToAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IntroToAPI.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionRepository(NZWalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<RegionModelResponse> AddRegion(Region obj)
        {
            var isRegionExist = await _dbContext.Regions.Where(x => x.Code == obj.Code).FirstOrDefaultAsync();

            try
            {
                if (isRegionExist == null)
                {
                    var region = new Region()
                    {
                        Code = obj.Code,
                        Name = obj.Name,
                        RegionImageUrl = obj.RegionImageUrl
                    };

                    await _dbContext.Regions.AddAsync(region);
                    await _dbContext.SaveChangesAsync();

                    return new RegionModelResponse() { Success = true, Message="Successfully Inserted New Region" };

                }


                else
                {
                    return new RegionModelResponse() { Success = false, Message = "The Code you entered was already exist" };
                }
            }

            catch(Exception ex)
            {
                return new RegionModelResponse() { Success=false, Message=ex.Message }; 
            }
            
        }

        public async Task<List<Region>> DisplayRegion()
        {
            var regionList = await _dbContext.Regions.ToListAsync();
            return regionList;  
        }

        public async Task<List<Region>> FilterRegion(string code)
        {
            try
            {
                var region = _dbContext.Regions.AsQueryable();

                if(string.IsNullOrWhiteSpace(code) == false)
                {
                    region = region.Where(x => x.Code.Contains(code));
                    return await region.ToListAsync();
                }

                return null;
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Region> ModifyRegionDetails(Region obj)
        {
            var isRegionExist = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Code == obj.Code);

            try
            {
                if (isRegionExist != null)
                {
                    isRegionExist.Code = obj.Code;
                    isRegionExist.Name = obj.Name;
                    isRegionExist.RegionImageUrl = obj.RegionImageUrl;

                    _dbContext.Update(isRegionExist);
                    await _dbContext.SaveChangesAsync();

                    return isRegionExist;
                }

                return null;
            }

            catch(Exception ex)
            {
                return null;
            }
           
        }

        public async Task<Region?> RegionByCode(string code)
        {
            var regionCode = await _dbContext.Regions.Where(x => x.Code == code).FirstOrDefaultAsync();
            return regionCode;
        }

        public async Task<List<Region>> SortRegion(string sortBy, bool ascending)
        {
            try
            {
                var region = _dbContext.Regions.AsQueryable();

                if(string.IsNullOrWhiteSpace(sortBy) == false)
                {
                    if(sortBy.Equals("Code"))
                    {
                        region = ascending ? region.OrderBy(x=>x.Code) : region.OrderByDescending(x=>x.Code);
                        return await region.ToListAsync();
                    }

                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
