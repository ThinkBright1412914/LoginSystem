using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Idenitity
{
    public class Cinemas : ICinemas
    {
        private readonly ApplicationDbContext _context;

        public Cinemas(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CinemaDto>> GetCinemas()
        {
            try
            {
                var response = _context.Cinemas.ToList();
                var cinema = new List<CinemaDto>();

                if (response.Any())
                {
                    foreach (var item in response)
                    {
                        cinema.Add(new CinemaDto
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Location = item.Location,
                            City = item.City,
                        });
                    }
                    return cinema;
                }
                else
                {
                    return new List<CinemaDto>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CinemaDto> GetCinemaById(int id)
        {
            CinemaDto response = new();
            try
            {
                var result = _context.Cinemas
                            .FirstOrDefault(x => x.Id == id);

                if (result != null)
                {
                    response.Id = result.Id;
                    response.Name = result.Name;
                    response.Location = result.Location; 
                    response.City = result.City;
                }
                else
                {
                    response.Message = "Id was not found";
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }

        public async Task<CinemaDto> Create(CinemaDto request)
        {

            CinemaDto response = new();
            try
            {
                Cinema model = new()
                {
                    Name = request.Name,
                    Location = request.Location,
                    City = request.City,
                };
                _context.Cinemas.Add(model);
                await _context.SaveChangesAsync();
                response.Message = "Created Successfully.";
            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }

        public async Task<CinemaDto> Delete(int id)
        {
            CinemaDto response = new();
            try
            {
                var result = _context.Cinemas
                            .FirstOrDefault(x => x.Id == id);

                if (result != null)
                {
                    _context.Cinemas.Remove(result);
                    _context.SaveChanges();
                    response.Message = "Deleted Successfully.";
                }
                else
                {
                    response.Message = "Id was not found";
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }  

        public async Task<CinemaDto> Update(CinemaDto request)
        {
            CinemaDto response = new();
            try
            {
                var result = _context.Cinemas.Find(request.Id);
                if (result != null)
                {
                    result.Name = request.Name;
                    result.Location = request.Location;
                    result.City = request.City;

                    _context.Cinemas.Update(result);
                    _context.SaveChanges();
                    response.Message = "Updated Successfully.";
                }
                else
                {
                    response.Message = "Id was not found";
                }

            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }
    }
}
