using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;


namespace DateSwipe.Server.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;

        public ProfileService(DatingDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<ProfileDTO>> GetProfile()
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == _authService.GetUserId());
            return new ServiceResponse<ProfileDTO>()
            {
                Data = new ProfileDTO()
                {
                    CoupleId = user.CoupleId,
                    DateCreated = user.DateCreated,
                    Email = user.Email,
                    ProfilePicture = user.ProfilePictureUrl,
                    UserName = user.Name,
                    Role = user.Role,
                    UserSwipes = user.UserSwipes,
                }
            };
        }

        public async Task<ServiceResponse<ProfileDTO>> GetPartnerProfile()
        {
            var user = await _context.Users.FindAsync(_authService.GetUserId());
            if (user == null || !user.CoupleId.HasValue)
            {
                return new ServiceResponse<ProfileDTO>()
                {
                    Data = null,
                    Success = false,
                    Message = "You are not part of a couple! Invite your partner!"
                };
            }

            var partner = await _context.Users
                .FirstOrDefaultAsync(u => u.CoupleId == user.CoupleId && u.Id != _authService.GetUserId());

            return new ServiceResponse<ProfileDTO>()
            {
                Data = new ProfileDTO()
                {
                    CoupleId = partner.CoupleId,
                    DateCreated = partner.DateCreated,
                    Email = partner.Email,
                    ProfilePicture = partner.ProfilePictureUrl,
                    UserName = partner.Name,
                    Role = partner.Role,
                    UserSwipes = partner.UserSwipes,
                }
            };
        }

        public async Task<ServiceResponse<ProfileDTO>> GetInviterProfileAsync(string token)
        {
            var invitation = await _context.Invitations
                .FirstOrDefaultAsync(i => i.Token == token && i.Expiration > DateTime.Now);

            if (invitation == null)
                return new ServiceResponse<ProfileDTO> { Success = false, Message = "Invalid or expired token." };

            var invitingUser = await _context.Users
                .Where(u => u.Id == invitation.UserId)
                .Select(u => new ProfileDTO
                {
                    UserName = u.Name,
                    ProfilePicture = u.ProfilePictureUrl
                })
                .FirstOrDefaultAsync();

            if (invitingUser == null)
                return new ServiceResponse<ProfileDTO> { Success = false, Message = "Inviting user not found." };

            return new ServiceResponse<ProfileDTO> { Data = invitingUser, Success = true };
        }


        public async Task<bool> UpdateProfile(User updatedUser)
        {
            var user = await _context.Users.FindAsync(updatedUser.Id);
            if (user == null)
            {
                return false;
            }

            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;
            user.IsSubscribed = updatedUser.IsSubscribed;
            user.CoupleId = updatedUser.CoupleId;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ServiceResponse<string>> UpdateProfilePictureUrl(string fileUrl)
        {
            var user = await _context.Users.FindAsync(_authService.GetUserId());
            if (user == null)
            {
                return new ServiceResponse<string> { Success = false, Message = "User not found." };
            }

            user.ProfilePictureUrl = fileUrl;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<string> { Success = true, Data = fileUrl };
        }

    }
}
