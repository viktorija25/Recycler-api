using Microsoft.EntityFrameworkCore;
using Recycler.API.RecyclerModels;
using Recycler.API.Utils;
using Recycler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recycler.API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMailer mailer;
        private readonly Random random = new Random();

        public AccountRepository( IMailer _mailer)
        {
            mailer = _mailer;
        }

        public int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public async Task<List<Account>> GetAllUsers()
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                List<Account> allusers = db.Account.Include(s => s.AccountAddress).ThenInclude(s => s.CityNavigation).ToList();
                return allusers;
            }
        }



        #region REGISTER ACCOUNT
        public async Task<Account> RegisterAccount(Account account)
        {
            await using(RecyclerContext db = new RecyclerContext())
            {
                db.Account.Add(account);
                db.SaveChanges();

                var email = EncryptionHelper.Decrypt(account.EmailAddress);
                var name = account.FirstName + " " + account.LastName;
                var password = EncryptionHelper.Decrypt(account.Password);
                int code = RandomNumber(100000, 999999);
                await mailer.SendEmailAsync(email, "Welcome to Recycler App, " + name," To activate your account enter the code: "+code);
                account.ActivationCode = code;
                db.SaveChanges();
                return account;
            }
        }
        #endregion

        #region LOGIN
        public async Task<Account> Login(string email, string password)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                var acc = db.Account.Where(u => u.EmailAddress == email && u.Password == password).FirstOrDefault();
                var active = acc.IsActive;
                if (active == 1)
                {
                    return acc;
                }
                else return null;
                  
            }
        }
        #endregion

        #region FORGOT PASSWORD
        public async Task<bool> ForgotPassword(string email)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                var account = db.Account.Where(a => a.EmailAddress == email).FirstOrDefault();
                if (account != null)
                {
                    int code = RandomNumber(100000, 999999);
                    var sendemail = EncryptionHelper.Decrypt(account.EmailAddress);
                    await mailer.SendEmailAsync(sendemail, "Forgot Password", "Please enter the code to confirm " + code);
                    account.PasswordCode = code;
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        #endregion

        #region RESET PASSWORD
        public async Task<bool> ResetPassword(int code , string newPassword)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                var account = db.Account.Where(a => a.PasswordCode == code).FirstOrDefault();
                if (account != null)
                {
                    account.Password = newPassword;
                    db.SaveChanges();
                    newPassword= EncryptionHelper.Decrypt(newPassword);
                    var email = EncryptionHelper.Decrypt(account.EmailAddress);
                    await mailer.SendEmailAsync(email, "Password reset", "Your password has been successfully reset " + newPassword);
                    return true;
                }
                else return false;
            }
        }
        #endregion

        #region CHANGE PASSWORD
        public async Task<bool> ChangePassword(string email ,string oldPassword, string newPassword)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                var account = db.Account.Where(a => a.Password == oldPassword&&a.EmailAddress==email).FirstOrDefault();
                if (account != null)
                {
                    account.Password = newPassword;
                    db.SaveChanges();
                    newPassword = EncryptionHelper.Decrypt(newPassword);
                    var sendemail = EncryptionHelper.Decrypt(account.EmailAddress);
                    await mailer.SendEmailAsync(sendemail, "Password change", "Your password has been successfully changed " + newPassword);
                    return true;
                }
                else return false;
            }
        }
        #endregion

        #region ACTIVATE ACCOUNT
        public async Task<bool> ActivateAccount(string email, int code)
        {
            await using (RecyclerContext db = new RecyclerContext())
            {
                var account = db.Account.Where(a => a.ActivationCode == code) .FirstOrDefault();
                if (account != null)
                {
                    account.IsActive=1;
                    account.AccountStatus = 2;
                    db.SaveChanges();
                    var pass = EncryptionHelper.Decrypt(account.Password);
                    var sendemail = EncryptionHelper.Decrypt(account.EmailAddress);
                    await mailer.SendEmailAsync(sendemail, "Account Activated", "Your Account is activated. Your password to login is: " + pass);
                    return true;
                }
                else return false;
            }
        }
        #endregion

        public List<Account> GetAccountDataTable(PaginationFilter filter)
        {
            using (RecyclerContext db = new RecyclerContext())
            {
                if (filter.SortDirection.ToLower() == "asc")
                {
                    switch (filter.SortColumn)
                    {
                       
                        case "date":
                            {
                                return db.Account.Where(p => (p.DateCreated >= filter.dateFrom) && (p.DateCreated <= filter.dateTo))
                                                  .OrderBy(x => x.DateCreated)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.AccountStatusNavigation).Include(b => b.AccountTypeNavigation).Include(a=>a.AccountAddress).ThenInclude(c=>c.CityNavigation)
                                                  .ToList();
                            }
              
                        default:
                            {
                                return db.Account.Where(p => (filter.SizeFrom == 0 || p.AccountId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.AccountId <= filter.SizeTo))
                                                  .OrderBy(x => x.AccountId)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.AccountStatusNavigation).Include(b => b.AccountTypeNavigation).Include(a => a.AccountAddress).ThenInclude(c => c.CityNavigation)
                                                  .ToList();
                            }
                    }
                }
                else
                {
                    switch (filter.SortColumn)
                    {
                       
                        case "date":
                            {
                                return db.Account.Where(p => (p.DateCreated >= filter.dateFrom) && (p.DateCreated <= filter.dateTo))
                                                  .OrderByDescending(x => x.DateCreated)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.AccountStatusNavigation).Include(b => b.AccountTypeNavigation).Include(a => a.AccountAddress).ThenInclude(c => c.CityNavigation)
                                                  .ToList();
                            }
                      
                        default:
                            {
                                return db.Account.Where(p => (filter.SizeFrom == 0 || p.AccountId >= filter.SizeFrom) && (filter.SizeTo == 0 || p.AccountId <= filter.SizeTo))
                                                  .OrderByDescending(x => x.AccountId)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize).Include(b => b.AccountStatusNavigation).Include(b => b.AccountTypeNavigation).Include(a => a.AccountAddress).ThenInclude(c => c.CityNavigation)
                                                  .ToList();
                            }
                    }
                }
            }
        }

        public int GetAccountCount(decimal sizeFrom, decimal sizeTo)
        {
            using (RecyclerContext db = new RecyclerContext())
            {
                return db.Account.Where(p => (sizeFrom == 0 || p.AccountId >= sizeFrom) && (sizeTo == 0 || p.AccountId <= sizeTo)).Count();
            }
        }
    }
}
