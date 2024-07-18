using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IGenre
    {
        Task<List<GenreDto>> GetGenres();
        Task<GenreDto> Create(GenreDto request);
        Task<GenreDto> Delete(int id);
    }
}
