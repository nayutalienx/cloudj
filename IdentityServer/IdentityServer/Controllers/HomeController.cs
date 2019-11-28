using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using IdentityServer.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdentityContext _identityContext;
        
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(
            IdentityContext identityContext,
            PersistedGrantDbContext persistedGrantDbContext,
            ConfigurationDbContext configurationDbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _identityContext = identityContext;
            
            _configurationDbContext = configurationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index() 
        {

            return View(); 
        }

        public async Task<IActionResult> InitializeDatabase(CancellationToken cancellationToken = default)
        {
            

            #region ConfigurationDbContext


            if (!await _configurationDbContext.Clients.AnyAsync(cancellationToken))
            {
                await _configurationDbContext.AddRangeAsync(Configuration.GetClients(), cancellationToken);
     
            }

            if (!await _configurationDbContext.IdentityResources.AnyAsync(cancellationToken))
            {
                
                await _configurationDbContext.IdentityResources.AddRangeAsync(Configuration.GetIdentityResources(), cancellationToken);
            }

            if (!_configurationDbContext.ApiResources.Any())
            {
                
                await _configurationDbContext.ApiResources.AddRangeAsync(Configuration.GetApiResources(), cancellationToken);
            }

            await _configurationDbContext.SaveChangesAsync(cancellationToken);

            #endregion

            #region IdentityUsersDbContext


            if (!await _identityContext.Users.AnyAsync(cancellationToken) && !await _identityContext.Roles.AnyAsync(cancellationToken))
            {
                

                foreach (var user in Configuration.GetIdentityUsers())
                    await _userManager.CreateAsync(user,user.Email); // [user object], [password: user.Email.toHash]


                await _identityContext.SaveChangesAsync(cancellationToken);
            }

            if (!await _identityContext.Roles.AnyAsync(cancellationToken))
            {
                
                foreach (var role in Configuration.GetIdentityRoles())
                    await _roleManager.CreateAsync(role);

                await _identityContext.SaveChangesAsync(cancellationToken);

                foreach (KeyValuePair<string, string> name_role in Configuration.GetUsernamesToRoles())
                    await _userManager.AddToRoleAsync(
                        await _userManager.FindByNameAsync(name_role.Key), name_role.Value
                    );

                await _identityContext.SaveChangesAsync(cancellationToken);
            }



            #endregion

            return View("Index");
        }
    }
}