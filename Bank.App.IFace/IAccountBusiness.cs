using Bank.App.Model;

namespace Bank.App.IFace;
public interface IAccountBusiness
{
    Task<bool> CreateAccount(Account entity);
    Task<IList<Account>> GetAccountByUserID(Account entity);
    Task<bool> DeleteAccount(Account entity);
    Task<Account> Update(Account entity);
    Task<Account> GetAccountByID(Account entity);
    

}

