using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IShowTime
    {
        Task<List<ShowTimeDto>> GetShowTime();
        Task<ShowTimeDto> Create(ShowTimeDto request);
        Task<ShowTimeDto> Delete(int id);
    }
}
