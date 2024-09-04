using Recycler.API.RecyclerModels;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API
{
   public  interface IAccountRepository
    {
        Task<Account> RegisterAccount(Account account);
        Task<Account> Login(string email, string password);
        Task<bool> ForgotPassword(string email);
        Task<bool> ResetPassword(int code, string newPassword);
        Task<bool> ChangePassword(string email, string oldPassword, string newPassword);
        Task<bool> ActivateAccount(string email, int code);

        Task<List<Account>> GetAllUsers();

        List<Account> GetAccountDataTable(PaginationFilter filter);
        int GetAccountCount(decimal sizeFrom, decimal sizeTo);

    }
}
