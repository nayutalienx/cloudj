using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.DataAccessLayer
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        DbSet<PhotoCaptcha> PhotoCaptchas { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            
        }
    }
}
