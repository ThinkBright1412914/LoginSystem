using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity
{
    public class ShowTimes : IShowTime
    {
        private readonly ApplicationDbContext _context; 

        public ShowTimes(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShowTimeDto>> GetShowTime()
        {
            try
            {
                var response = _context.ShowTime.ToList();
                var showTime = new List<ShowTimeDto>();
                if (response.Any())
                {
                    foreach (var items in response)
                    {
                        showTime.Add(new ShowTimeDto
                        {
                            Id = items.Id,
                            Time = items.Time,
                        });
                    }
                    return showTime;
                }
                else
                {
                    return new List<ShowTimeDto> { };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ShowTimeDto> Create(ShowTimeDto request)
        {
            try
            {
                if (request != null)
                {
                    ShowTime model = new()
                    {
                        Time = request.Time,
                    };
                    _context.ShowTime.Add(model);
                    await _context.SaveChangesAsync();
                    return new ShowTimeDto { Message = "Created Successfully" };
                }
                else
                {
                    return new ShowTimeDto();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        public async Task<ShowTimeDto> Delete(int id)
        {
            try
            {
                var response = _context.ShowTime.Find(id);
                if (response != null)
                {
                    _context.ShowTime.Remove(response);
                    await _context.SaveChangesAsync();
                    return new ShowTimeDto { Message = "Delete Successfully" };
                }
                else
                {
                    return new ShowTimeDto { Message = "Id was not found." };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
