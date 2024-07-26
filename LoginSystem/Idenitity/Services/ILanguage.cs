using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface ILanguage
    {
        Task<List<LanguageDto>> GetLanguages();
        Task<LanguageDto> Create(LanguageDto request);
        Task<LanguageDto> Delete(int id);
    }
}
