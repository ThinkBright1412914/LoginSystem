using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Idenitity
{
	public class Movies : IMovies
	{
		private readonly ApplicationDbContext _context;

		public Movies(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<MovieDto>> GetMovies(string? filterMovies)
		{
			try
			{
				var response = _context.Movies
								.Include(x => x.Genre)
								.Include(x => x.Language)
								.Include(x => x.Industry)
								.ToList();
				var movie = new List<MovieDto>();

				switch (filterMovies)
				{
					case "PremierMovies":
						response = response.Where(x => x.ReleaseDate <= DateTime.Now).ToList();
						break;

					case "UpcomingMovie":
						response = response.Where(x => x.ReleaseDate >= DateTime.Now).ToList();
						break;

					default:
						break;
				}

				if (response.Any())
				{
					foreach (var item in response)
					{
						movie.Add(new MovieDto
						{
							Id = item.Id,
							Name = item.Name,
							ReleaseDate = item.ReleaseDate.ToShortDateString(),
							Image = item.Image != null ? Convert.ToBase64String(item.Image) : null,
							Duration = item.Duration,
							GenreId = item.GenreId,
							IndustryId = item.IndustryId,
							LanguageId = item.LanguageId,
							GenresList = new List<GenreDto> { new GenreDto { Name = item.Genre.Name } },
							LanguageList = new List<LanguagugeDto> { new LanguagugeDto { Name = item.Language.Name } },
							IndustryList = new List<IndustryDto> { new IndustryDto { Name = item.Industry.Name } }
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
					response.ReleaseDate = result.ReleaseDate.ToShortDateString();
					response.Image = result.Image != null ? Convert.ToBase64String(result.Image) : "";
					response.GenreId = result.GenreId;
					response.LanguageId = result.LanguageId;
					response.IndustryId = result.IndustryId;
				}
				else
				{
					response.Message = "Id was not found";
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
			MovieDto response = new();
			try
			{
                Movie model = new()
				{
					Name = request.Name,
					Duration = request.Duration,
					ReleaseDate = DateTime.Parse(request.ReleaseDate),
					GenreId = request.GenreId,
					IndustryId = request.IndustryId,
					LanguageId = request.LanguageId,
					Image = new Converter().ConvertBase64ToByteArray(request.Image),
				};
				_context.Movies.Add(model);
				await _context.SaveChangesAsync();
				response.Message = "Created Successfully.";
			}
			catch (Exception e)
			{
				throw;
			}
			return response;
		}

		public async Task<MovieDto> Delete(int id)
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
					_context.Movies.Remove(result);
					_context.SaveChanges();
					response.Message = "Deleted Successfully.";
				}
				else
				{
					response.Message = "Id was not found";
				}
			}
			catch (Exception e)
			{
				throw;
			}
			return response;
		}

		public async Task<MovieDto> Update(MovieDto request)
		{
			MovieDto response = new();
			try
			{
				var result = _context.Movies.Find(request.Id);
				if (result != null)
				{
					result.Name = request.Name;
					result.Duration = request.Duration;
					result.ReleaseDate = DateTime.Parse(request.ReleaseDate);
					result.GenreId = request.GenreId;
					result.IndustryId = request.IndustryId;
					result.LanguageId = request.LanguageId;

					_context.Movies.Update(result);
					_context.SaveChanges();
					response.Message = "Updated Successfully.";
				}
				else
				{
					response.Message = "Id was not found";
				}

			}
			catch (Exception e)
			{
				throw;
			}
			return response;
		}
	}
}
