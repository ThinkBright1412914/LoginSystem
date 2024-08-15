using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IGenre
    {
        Task<List<GenreDto>> GetGenres();
        Task<GenreDto> Create(GenreDto request);
        Task<GenreDto> Delete(int id);
    }

	public class Genres : IGenre
	{
		private readonly ApplicationDbContext _context;

		public Genres(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<GenreDto>> GetGenres()
		{
			try
			{
				var response = _context.Genres.ToList();
				var genre = new List<GenreDto>();
				if (response.Any())
				{
					foreach (var items in response)
					{
						genre.Add(new GenreDto
						{
							Id = items.Id,
							Name = items.Name,
						});
					}
					return genre;
				}
				else
				{
					return new List<GenreDto> { };
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<GenreDto> Create(GenreDto request)
		{
			try
			{
				if (request != null)
				{
					Genre model = new()
					{
						Name = request.Name,
					};
					_context.Genres.Add(model);
					await _context.SaveChangesAsync();
					return new GenreDto { Message = "Created Successfully" };
				}
				else
				{
					return new GenreDto();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<GenreDto> Delete(int id)
		{
			try
			{
				var response = _context.Genres.Find(id);
				if (response != null)
				{
					_context.Genres.Remove(response);
					await _context.SaveChangesAsync();
					return new GenreDto { Message = "Delete Successfully" };
				}
				else
				{
					return new GenreDto { Message = "Id was not found." };
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}


	}
}
