using LoginSystem.Domain.Model;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity.Services
{
    public interface ICarousel
    {
        Task<List<CarouselDto>> GetCarousels();
        Task<CarouselDto> Create(CarouselDto request);
        Task<CarouselDto> Delete(int id);
    }
}
