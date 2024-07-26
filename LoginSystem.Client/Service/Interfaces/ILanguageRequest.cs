using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface ILanguageRequest
	{
		Task<List<LanguageDto>> GetAll();
		Task<LanguageDto> Create(LanguageDto model);
		Task<LanguageDto> Delete(int Id);
	}
}
