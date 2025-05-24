namespace Hospital_Mangment_System.repository
{
    public interface ITokenBlacklistService
    {
        Task<bool> IsTokenBlacklisted(string token);
        Task BlacklistToken(string token, DateTime expiry);
    }
}
