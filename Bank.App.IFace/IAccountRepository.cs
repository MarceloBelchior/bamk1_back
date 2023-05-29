using Bank.App.Model;

namespace Bank.App.IFace;
public interface IAccountRepository
{
    Task<IList<Bank.App.Model.Account>> GetUserById(Bank.App.Model.Account entity);
    Task<bool> SaveOrUpdate(Model.Account entity);
    Task<Bank.App.Model.Account> GeById(Bank.App.Model.Account entity);

}

