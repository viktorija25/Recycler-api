using Recycler.API.RecyclerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public interface IAdressAccountRepository
    {
        Task<AccountAddress> CreateAccountAdress(AccountAddress accountAdress);
        Task<bool> UpdateAccountAdress(AccountAddress accountAdress);
        Task<AccountAddress> GetAccountAddressById(int id);
        Task<bool> DeleteAccountAddress(int id);
        Task<List<AccountAddress>> GetAllAccountAdress();



    }
}
