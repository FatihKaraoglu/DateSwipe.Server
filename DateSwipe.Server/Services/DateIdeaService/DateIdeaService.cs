﻿using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace DateSwipe.Server.Services.DateIdeaService
{
    public class DateIdeaService : IDateIdeaService
    {
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;

        public DateIdeaService(DatingDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<List<DateIdeaDTO>>> GetDateIdeasAsync()
        {
            var response = new ServiceResponse<List<DateIdeaDTO>>();
            var userId = _authService.GetUserId();

            try
            {
                var swipedDateIds = await _context.UserSwipes
                    .Where(us => us.UserId == userId)
                    .Select(us => us.DateIdeaId)
                    .ToListAsync();

                var unswipedDateIdeas = await _context.DateIdeas
                    .Where(di => !swipedDateIds.Contains(di.Id))
                    .Include(di => di.DateIdeaCategories)
                        .ThenInclude(dic => dic.Category)
                .ToListAsync();

                var dateIdeaDtos = unswipedDateIdeas.Select(di => new DateIdeaDTO
                {
                    Id = di.Id,
                    Title = di.Title,
                    Description = di.Description,
                    ImageUrl = di.ImageUrl,
                    Categories = di.DateIdeaCategories.Select(dic => new CategoryDto
                    {
                        Id = dic.Category.Id,
                        Name = dic.Category.Name
                    }).ToList()
                }).ToList();

                response.Data = dateIdeaDtos;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<DateIdeaDTO>> GetDateIdeaByIdAsync(int id)
        {
            var response = new ServiceResponse<DateIdeaDTO>();

            try
            {
                var dateIdea = await _context.DateIdeas
                    .Include(di => di.DateIdeaCategories)
                        .ThenInclude(dic => dic.Category)
                    .FirstOrDefaultAsync(di => di.Id == id);

                if (dateIdea == null)
                {
                    response.Success = false;
                    response.Message = "Date idea not found.";
                    return response;
                }

                var dateIdeaDto = new DateIdeaDTO
                {
                    Id = dateIdea.Id,
                    Title = dateIdea.Title,
                    Description = dateIdea.Description,
                    ImageUrl = dateIdea.ImageUrl,
                    Categories = dateIdea.DateIdeaCategories.Select(dic => new CategoryDto
                    {
                        Id = dic.Category.Id,
                        Name = dic.Category.Name
                    }).ToList()
                };

                response.Data = dateIdeaDto;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> SwipeAsync(int dateId, bool liked)
        {
            var response = new ServiceResponse<string>();
            var userId = _authService.GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }

            var coupleId = user.CoupleId;

            if (!coupleId.HasValue)
            {
                response.Success = false;
                response.Message = "User is not part of a couple.";
                return response;
            }

            try
            {
                var userSwipe = new UserSwipe
                {
                    UserId = userId,
                    DateIdeaId = dateId,
                    CoupleId = coupleId.Value,
                    Liked = liked
                };

                _context.UserSwipes.Add(userSwipe);
                await _context.SaveChangesAsync();

                // Check for a match
                if (liked)
                {
                    var isMatch = await _context.UserSwipes.AnyAsync(us =>
                        us.DateIdeaId == dateId &&
                        us.CoupleId == coupleId.Value &&
                        us.UserId != userId &&
                        us.Liked);

                    if (isMatch)
                    {
                        response.Data = "It's a match!";
                    }
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
