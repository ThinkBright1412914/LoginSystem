using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service.Interfaces
{
    public interface IMovieRequest
    {
        Task<List<MovieDto>> GetMovies();
        Task<MovieDto> GetMovieById(int id);
        Task<MovieDto> Create(MovieDto model);
        Task<MovieDto> Update(MovieDto model);
        Task<MovieDto> Delete(int Id);
    }
}
