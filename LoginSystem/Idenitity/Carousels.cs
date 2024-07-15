using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.Utility;
using LoginSystem.ViewModel;

namespace LoginSystem.Idenitity
{
    public class Carousels : ICarousel
    {
        private readonly ApplicationDbContext _context;

        public Carousels(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<CarouselDto>> GetCarousels()
        {
            try
            {
                var response = _context.Carousels.ToList();
                var carousel = new List<CarouselDto>();
                if (response.Any())
                {
                    foreach (var carousels in response)
                    {
                        carousel.Add(new CarouselDto
                        {
                            Id = carousels.Id,
                            Image = Convert.ToBase64String(carousels.Image)
                        });
                    }
                    return carousel;
                }
                else
                {
                    return new List<CarouselDto> { };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CarouselDto> Create(CarouselDto request)
        {
            try
            {
                if (request != null)
                {
                    var img = new Converter().ConvertBase64ToByteArray(request.Image);
                    Carousel model = new()
                    {
                        Image = img
                    };
                    _context.Carousels.Add(model);
                    await _context.SaveChangesAsync();
                    return new CarouselDto { Message = "Created Successfully" };
                }
                else
                {
                    return new CarouselDto();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CarouselDto> Delete(int id)
        {
            try
            {
                var response = _context.Carousels.Find(id);
                if(response != null)
                {
                   _context.Carousels.Remove(response);
                   await _context.SaveChangesAsync();
                    return new CarouselDto { Message = "Delete Successfully" };
                }
                else
                {
                    return new CarouselDto { Message = "Id was not found." };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
