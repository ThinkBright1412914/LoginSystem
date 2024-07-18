using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service.Interfaces
{
    public interface IGenreRequest
    {
        Task<List<GenreDto>> GetAll();
        Task<GenreDto> Create(GenreDto model);
        Task<GenreDto> Delete(int Id);
    }
}
