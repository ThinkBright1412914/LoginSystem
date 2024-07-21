using LoginSystem.DTO;
using LoginSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Idenitity.Services
{
	public class Movie : IMovie
	{
		private readonly ApplicationDbContext _context;

		public Movie(ApplicationDbContext context)
		{
			_context = context;	
		}

		public async Task<List<MovieDto>> GetMoviess()
		{
			try
			{
				var response = _context.Movies.ToList();
				var movie = new List<MovieDto>();
				if (response.Any())
				{
					foreach (var item in response)
					{
						movie.Add(new MovieDto
						{
							Id = item.Id,
							Name = item.Name,
							ReleaseDate = item.ReleaseDate,
							Image = item.Image != null ? Convert.ToBase64String(item.Image) : null,
							Duration = item.Duration,
							GenreId = item.GenreId,
							IndustryId = item.IndustryId,
							LanguageId = item.LanguageId,
						});
					}
					return movie;
				}
				else
				{
					return new List<MovieDto> { };
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<MovieDto> GetMovieById(int id)
		{
			MovieDto response = new();
			try
			{
				var result = _context.Movies
							.Include(x => x.Genre)
							.Include(x => x.Industry)
							.Include(x => x.Language)
							.FirstOrDefault(x => x.Id == id);

				if (result != null)
				{
					response.Id = result.Id;
					response.Name = result.Name;
					response.Duration = result.Duration;
					response.ReleaseDate = result.ReleaseDate;
					response.Image = result.Image != null ? Convert.ToBase64String(result.Image) : "";
					response.GenreId = result.GenreId;
					response.LanguageId = result.LanguageId;
					response.IndustryId = result.IndustryId;
				}
			}
			catch (Exception e)
			{
				throw;
			}
			return response;
		}

		public async Task<MovieDto> Create(MovieDto request)
		{
			throw new NotImplementedException();
		}

		public async Task<MovieDto> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<MovieDto> Update(MovieDto request)
		{
			throw new NotImplementedException();
		}	
	}
}
