using Server.Models;
using Server.Repositories.Register;

namespace Server.Services.Register
{
    public class RegisterService:IRegisterService
    {
        private readonly IRegisterRepository _registerrepository;
        public RegisterService(IRegisterRepository registerrepository)
        {
            _registerrepository = registerrepository;
        }
        public void AddUser(User user)
        {
            _registerrepository.AddUser(user);
        }
    }
}
