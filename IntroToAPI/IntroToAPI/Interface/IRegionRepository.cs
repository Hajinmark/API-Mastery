using IntroToAPI.ModelResponse;
using IntroToAPI.Models.Domain;

namespace IntroToAPI.Interface
{
    public interface IRegionRepository
    {
        Task<RegionModelResponse> AddRegion(Region obj);
        Task<List<Region>> DisplayRegion();
        Task<Region?> RegionByCode(string code);
        Task<Region> ModifyRegionDetails(Region obj);
        Task<List<Region>> FilterRegion(string code);
        Task<List<Region>> SortRegion(string sortBy, bool ascending);
    }
}
