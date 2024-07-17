using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service.Interfaces
{
    public interface ICarouselRequest
    {
        Task<List<CarouselDto>> GetAll();
        Task<CarouselDto> Create(CarouselDto model);
        Task<CarouselDto> Delete(int Id);
    }
}
