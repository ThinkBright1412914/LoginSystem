using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity
{
    public class Industrys : IIndustry
    {
        private readonly ApplicationDbContext _context;

        public Industrys(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IndustryDto>> GetIndutries()
        {
            try
            {
                var response = _context.Industries.ToList();
                var industry = new List<IndustryDto>();
                if (response.Any())
                {
                    foreach (var items in response)
                    {
                        industry.Add(new IndustryDto
                        {
                            Id = items.Id,
                            Name = items.Name,
                        });
                    }
                    return industry;
                }
                else
                {
                    return new List<IndustryDto> { };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IndustryDto> Create(IndustryDto request)
        {
            try
            {
                if (request != null)
                {
                    Industry model = new()
                    {
                        Name = request.Name,
                    };
                    _context.Industries.Add(model);
                    await _context.SaveChangesAsync();
                    return new IndustryDto { Message = "Created Successfully" };
                }
                else
                {
                    return new IndustryDto();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IndustryDto> Delete(int id)
        {
            try
            {
                var response = _context.Industries.Find(id);
                if (response != null)
                {
                    _context.Industries.Remove(response);
                    await _context.SaveChangesAsync();
                    return new IndustryDto { Message = "Delete Successfully" };
                }
                else
                {
                    return new IndustryDto { Message = "Id was not found." };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
    }
}
