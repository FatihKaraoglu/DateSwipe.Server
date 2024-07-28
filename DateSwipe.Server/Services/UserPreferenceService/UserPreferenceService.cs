using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateSwipe.Server.Services.UserPreferenceService
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;

        public UserPreferenceService(DatingDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<bool>> SetCategoryPreferenceAsync(int categoryId, bool liked)
        {
            var response = new ServiceResponse<bool>();
            var userId = _authService.GetUserId();

            try
            {
                var preference = await _context.UserCategoryPreferences
                    .FirstOrDefaultAsync(p => p.UserId == userId && p.CategoryId == categoryId);

                if (preference == null)
                {
                    preference = new UserCategoryPreference
                    {
                        UserId = userId,
                        CategoryId = categoryId,
                        Liked = liked
                    };
                    _context.UserCategoryPreferences.Add(preference);
                }
                else
                {
                    preference.Liked = liked;
                }

                await _context.SaveChangesAsync();
                response.Data = true;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<UserPreferencesDTO>>> GetLikedCategoriesAsync()
        {
            var response = new ServiceResponse<List<UserPreferencesDTO>>();
            var userId = _authService.GetUserId();

            try
            {
                var likedCategories = await _context.UserCategoryPreferences
                    .Where(p => p.UserId == userId && p.Liked)
                    .Include(p => p.Category)
                    .Select(p => new UserPreferencesDTO
                    {
                        CategoryId = p.Category.Id,
                        CategoryName = p.Category.Name,
                        IsLiked = p.Liked
                    })
                    .ToListAsync();

                response.Data = likedCategories;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<UserPreferencesDTO>>> GetAllCategoryPrefernces()
        {
            var response = new ServiceResponse<List<UserPreferencesDTO>>();
            var userId = _authService.GetUserId();

            try
            {
                var allCategories = await _context.Categories
                    .Include(c => c.UserPreferences)
                    .Select(c => new UserPreferencesDTO
                    {
                        CategoryId = c.Id,
                        CategoryName = c.Name,
                        IsLiked = c.UserPreferences.Any(p => p.UserId == userId && p.Liked)
                    })
                    .ToListAsync();

                response.Data = allCategories;
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
