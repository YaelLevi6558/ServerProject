using Server.Models;

namespace Server.Repositories.Register
{
    public class RegisterRepository:IRegisterRepository
    {
        private readonly ChineseAuctionContext _context;
        public RegisterRepository(ChineseAuctionContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = password;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
