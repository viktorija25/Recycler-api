using Recycler.API.RecyclerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public class AdressAccountRepository:IAdressAccountRepository
    {
        #region CREATE
        public async Task<AccountAddress> CreateAccountAdress(AccountAddress accountAdress)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.AccountAddress.Add(accountAdress);
                db.SaveChanges();
                return accountAdress;
            }
        }
        #endregion

        #region UPDATE
        public async Task<bool> UpdateAccountAdress(AccountAddress accountAdress)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                db.AccountAddress.Update(accountAdress);
                db.SaveChanges();
                return true;
            }
        }
        #endregion

        #region GET BY ID
        public async Task<AccountAddress> GetAccountAddressById(int id)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                var accAddress = db.AccountAddress.Where(b => b.AccountId == id).FirstOrDefault();
                return accAddress;
            }
        }
        #endregion
        
        #region DELETE
        public async Task<bool> DeleteAccountAddress(int id)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                AccountAddress accAddress = db.AccountAddress.Where(p => p.AccountId == id).FirstOrDefault();
                db.AccountAddress.Remove(accAddress);
                await db.SaveChangesAsync();
                return true;
            }
        }
        #endregion

        #region GET ALL 
        public async Task<List<AccountAddress>> GetAllAccountAdress()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                return db.AccountAddress.ToList();
            }
        }
        #endregion
    }
}
