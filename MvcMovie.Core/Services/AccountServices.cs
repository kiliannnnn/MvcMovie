using MvcMovie.Core.Helpers;
using MvcMovie.Core.ServiceHelpers;
using MvcMovie.Core.ViewModels;
using MvcMovie.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMovie.Core.Services
{
    public class AccountService : EFServiceBase<MvcMovieContext, UserAccount>
    {
        public AccountService(IContextProvider contextProvider) : base(contextProvider)
        {
        }

        public async Task<AccountManageModel> GetAccountManageModelByUserIdAsync(int userId)
        {
            UserAccount user = await Context.UserAccounts.FindAsync(userId);

            return new AccountManageModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                // Notify = user.Notify
                // BCUserId = user.BCUserId,
            };
        }

        public async Task SaveAsync(int userId, AccountManageModel model)
        {
            UserAccount user = await Context.UserAccounts.FindAsync(userId);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            // user.Notify = model.Notify;
            user.UpdateDate = DateTime.Now;

            await Context.SaveChangesAsync();
        }
    }
}
