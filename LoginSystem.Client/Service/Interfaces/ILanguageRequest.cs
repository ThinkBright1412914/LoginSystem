using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface ILanguageRequest
	{
		Task<List<LanguagugeDto>> GetAll();
		Task<LanguagugeDto> Create(LanguagugeDto model);
		Task<LanguagugeDto> Delete(int Id);
	}
}
