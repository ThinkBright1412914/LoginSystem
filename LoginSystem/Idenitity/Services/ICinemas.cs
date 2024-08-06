using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface ICinemas
    {
        Task<List<CinemaDto>> GetCinemas();
        Task<CinemaDto> GetCinemaById(int Id);
        Task<CinemaDto> Create(CinemaDto request);
        Task<CinemaDto> Update(CinemaDto request);
        Task<CinemaDto> Delete(int id);
    }
}
