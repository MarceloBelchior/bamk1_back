using System.Threading.Tasks;
using Bank.App.Model;

namespace Bank.App.IFace;
public interface IUserRepository
{
  Task<Bank.App.Model.User> GetUserById(Bank.App.Model.User entity);
  Task<bool> SaveOrUpdate(Model.User entity);
  Task<List<Bank.App.Model.User>> GetListUser();

}

