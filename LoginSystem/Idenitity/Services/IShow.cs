using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IShow
    {
        Task<List<ShowDto>> GetShows();
        Task<ShowDto> GetShowById(int Id);
        Task<ShowDto> Create(ShowDto request);
        Task<ShowDto> Update(ShowDto request);
        Task<ShowDto> Delete(int id);
    }
}
