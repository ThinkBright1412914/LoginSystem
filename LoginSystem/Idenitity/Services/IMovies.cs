using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
	public interface IMovies
	{
		Task<List<MovieDto>> GetMovies();
		Task<MovieDto> GetMovieById(int id);	
		Task<MovieDto> Create(MovieDto request);
		Task<MovieDto> Update(MovieDto request);
		Task<MovieDto> Delete(int id);
	}
}
