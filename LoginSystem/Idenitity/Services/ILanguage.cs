using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface ILanguage
    {
        Task<List<LanguagugeDto>> GetLanguages();
        Task<LanguagugeDto> Create(LanguagugeDto request);
        Task<LanguagugeDto> Delete(int id);
    }
}
