using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity
{
    public class Languages : ILanguage
    {
        private readonly ApplicationDbContext _context;

        public Languages(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LanguagugeDto>> GetLanguages()
        {
            try
            {
                var response = _context.Languages.ToList();
                var language = new List<LanguagugeDto>();
                if (response.Any())
                {
                    foreach (var items in response)
                    {
                        language.Add(new LanguagugeDto
                        {
                            Id = items.Id,
                            Name = items.Name
                        });
                    }
                    return language;
                }
                else
                {
                    return new List<LanguagugeDto> { };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LanguagugeDto> Create(LanguagugeDto request)
        {
            try
            {
                if (request != null)
                {
                    Language model = new()
                    {
                        Name = request.Name,
                    };
                    _context.Languages.Add(model);
                    await _context.SaveChangesAsync();
                    return new LanguagugeDto { Message = "Created Successfully" };
                }
                else
                {
                    return new LanguagugeDto();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LanguagugeDto> Delete(int id)
        {
            try
            {
                var response = _context.Languages.Find(id);
                if (response != null)
                {
                    _context.Languages.Remove(response);
                    await _context.SaveChangesAsync();
                    return new LanguagugeDto { Message = "Delete Successfully" };
                }
                else
                {
                    return new LanguagugeDto { Message = "Id was not found." };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
