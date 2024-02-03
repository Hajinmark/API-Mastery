using IntroToAPI.ModelResponse;
using IntroToAPI.Models.Domain;
using IntroToAPI.Models.ViewModel;

namespace IntroToAPI.Interface
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> ListDifficulty();
        Task<Difficulty> AddDifficulty(Difficulty obj);
        Task<Difficulty> UpdateDifficulty(Guid id, Difficulty obj);
        Task<List<Difficulty>> FilterDifficulty(string name);
        Task<List<Difficulty>> SortDifficulty(string sortyBy, bool ascending);
        Task<RegionModelResponse> AddWalk(WalkViewModel obj);
    }
}
