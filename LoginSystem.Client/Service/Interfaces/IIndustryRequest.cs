using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IIndustryRequest
	{
		Task<List<IndustryDto>> GetAll();
		Task<IndustryDto> Create(IndustryDto model);
		Task<IndustryDto> Delete(int Id);
	}
}
