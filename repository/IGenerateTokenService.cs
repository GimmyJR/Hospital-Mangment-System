using Hospital_Mangment_System.Models;

namespace Hospital_Mangment_System.repository
{
    public interface IGenerateTokenService
    {
        public Task<string> GenerateJwtTokenAsync(AppUser user);
    }
}
