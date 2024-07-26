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

        public async Task<List<LanguageDto>> GetLanguages()
        {
            try
            {
                var response = _context.Languages.ToList();
                var language = new List<LanguageDto>();
                if (response.Any())
                {
                    foreach (var items in response)
                    {
                        language.Add(new LanguageDto
                        {
                            Id = items.Id,
                            Name = items.Name
                        });
                    }
                    return language;
                }
                else
                {
                    return new List<LanguageDto> { };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LanguageDto> Create(LanguageDto request)
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
                    return new LanguageDto { Message = "Created Successfully" };
                }
                else
                {
                    return new LanguageDto();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LanguageDto> Delete(int id)
        {
            try
            {
                var response = _context.Languages.Find(id);
                if (response != null)
                {
                    _context.Languages.Remove(response);
                    await _context.SaveChangesAsync();
                    return new LanguageDto { Message = "Delete Successfully" };
                }
                else
                {
                    return new LanguageDto { Message = "Id was not found." };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
