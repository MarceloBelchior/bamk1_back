using Bank.App.IFace;
using Bank.App.Model;

namespace Bank.App.Business;
public class UserBusiness : IUserBusiness
{

    private readonly IUserRepository userRepository;
    public UserBusiness(IUserRepository _userRepository) => userRepository = _userRepository;

      public async Task<List<User>> GetUserActive()
    {
        return await userRepository.GetListUser();
    }
   
    public async Task<User> UpdateUser(User user)
    {
        return (await userRepository.SaveOrUpdate(user) ? user : null); 
    }
}

