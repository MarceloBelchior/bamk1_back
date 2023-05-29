using Bank.App.Model;

namespace Bank.App.IFace;
public interface IUserBusiness
{
     
      Task<List<User>> GetUserActive();
      Task<User> UpdateUser(User user);
   

}

