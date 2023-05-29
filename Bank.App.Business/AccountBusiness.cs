using Bank.App.IFace;
using Bank.App.Model;

namespace Bank.App.Business;
public class AccountBusiness : IAccountBusiness
{


    private readonly IAccountRepository accountRepository;

    public AccountBusiness(IAccountRepository _accountRepository) => accountRepository = _accountRepository;




    public async Task<bool> CreateAccount(Account entity)
    {
        entity.Active = true;
        return await accountRepository.SaveOrUpdate(entity);

    }
    public async Task<IList<Account>> GetAccountByUserID(Account entity)
    {
        return await accountRepository.GetUserById(entity);
    }


    public async Task<bool> DeleteAccount(Account entity)
    {
        var query = await accountRepository.GeById(entity);
        query.Active = false;
        return await accountRepository.SaveOrUpdate(entity);

    }

    public async Task<Account> Update(Account entity)
    {
        await accountRepository.SaveOrUpdate(entity);
        return entity;
    }

    public async Task<Account> GetAccountByID(Account entity)
    {
        return await accountRepository.GeById(entity);
    }


}

