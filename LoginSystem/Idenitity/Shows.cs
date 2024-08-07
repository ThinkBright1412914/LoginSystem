﻿using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Idenitity
{
    public class Shows : IShow
    {
        private readonly ApplicationDbContext _context;

        public Shows(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShowDto>> GetShows()
        {
            try
            {
                var response = _context.Shows
                                       .Include(x => x.MovieInfo)
                                       .Include(x => x.CinemaInfo)
                                       .Include(x => x.ShowTimeInfo)
                                       .ToList();
                var show = new List<ShowDto>();

                if (response.Any())
                {
                    foreach (var item in response)
                    {
                        show.Add(new ShowDto
                            {
                                Id = item.Id,
                                MovieId = item.MovieId,
                                CinemaId= item.CinemaId,
                                ShowDate = item.ShowDate.ToShortDateString(),
                                ShowTimeId = item.ShowTimeId,
                                TicketPrice = item.TicketPrice,
                                SeatNo = item.SeatNo,
                                MovieList = new List<SelectListItem> { new SelectListItem { Text = item.MovieInfo.Name } },
                                ShowTimeList = new List<SelectListItem> { new SelectListItem { Text = item.ShowTimeInfo.Time } },
                                CinemaList = new List<SelectListItem> { new SelectListItem { Text = item.CinemaInfo.Name } }
                            });
                    }
                    return show;
                }
                else
                {
                    return new List<ShowDto>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ShowDto> GetShowById(int Id)
        {
            ShowDto response = new();
            try
            {
                var result = _context.Shows
                                .Include(x => x.MovieInfo)
                                .Include(x => x.CinemaInfo)
                                .Include(x => x.ShowTimeInfo)
                                .FirstOrDefault(x => x.Id == Id);

                if (result != null)
                {
                    response.Id = result.Id;
                    response.MovieId = result.MovieId;
                    response.CinemaId = result.CinemaId;
                    response.ShowDate = result.ShowDate.ToShortDateString();
                    response.ShowTimeId = result.ShowTimeId;
                    response.TicketPrice = result.TicketPrice;
                    response.SeatNo = result.SeatNo;
                    response.MovieList = new List<SelectListItem> { new SelectListItem { Text = result.MovieInfo.Name } };
                    response.ShowTimeList = new List<SelectListItem> { new SelectListItem { Text = result.ShowTimeInfo.Time } };
                    response.CinemaList = new List<SelectListItem> { new SelectListItem { Text = result.CinemaInfo.Name } };
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

        public async Task<ShowDto> Create(ShowDto request)
        {
            ShowDto response = new();
            try
            {
                Show model = new()
                {
                    MovieId = request.MovieId,
                    CinemaId = request.CinemaId,
                    ShowDate = DateTime.Parse(request.ShowDate),
                    ShowTimeId = request.ShowTimeId,
                    TicketPrice = request.TicketPrice,
                    SeatNo = request.SeatNo,
                };
                _context.Shows.Add(model);
                await _context.SaveChangesAsync();
                response.Message = "Created Successfully.";
            }
            catch (Exception e)
            {
                throw;
            }
            return response;
        }

        public async Task<ShowDto> Delete(int id)
        {
            ShowDto response = new();
            try
            {
                var result = _context.Shows
                            .FirstOrDefault(x => x.Id == id);

                if (result != null)
                {
                    _context.Shows.Remove(result);
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

        public async Task<ShowDto> Update(ShowDto request)
        {
            ShowDto response = new();
            try
            {
                var result = _context.Shows.Find(request.Id);
                if (result != null)
                {
                    result.MovieId = request.MovieId;
                    result.CinemaId = request.CinemaId;
                    result.ShowTimeId = request.ShowTimeId;
                    result.SeatNo = request.SeatNo;
                    result.ShowDate = DateTime.Parse(request.ShowDate);
                    result.TicketPrice = request.TicketPrice;

                    _context.Shows.Update(result);
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
