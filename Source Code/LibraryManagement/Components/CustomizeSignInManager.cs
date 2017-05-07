using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LibraryManagement.Models;

namespace LibraryManagement.Components
{
    public class CustomizeSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly LibraryContext _db;
        private readonly IHttpContextAccessor _contextAccessor;

        public CustomizeSignInManager(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, LibraryContext dbContext,
            IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger) 
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public override async Task<SignInResult> PasswordSignInAsync(string identity, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var person = await UserManager.FindByNameAsync(identity);
            if (person == null)
            {
                person =  await UserManager.FindByEmailAsync(identity);
                if (person == null)
                    return SignInResult.Failed;
                else
                    return await PasswordSignInAsync(person, password, isPersistent, lockoutOnFailure);
            }
            return await PasswordSignInAsync(person, password, isPersistent, lockoutOnFailure);
        }
    }
}
