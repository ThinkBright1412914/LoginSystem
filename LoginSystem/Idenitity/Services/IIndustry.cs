using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface IIndustry
    {
        Task<List<IndustryDto>> GetIndutries();
        Task<IndustryDto> Create(IndustryDto request);
        Task<IndustryDto> Delete(int id);
    }
}
