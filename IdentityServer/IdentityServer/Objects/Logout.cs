using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Objects
{
    public class LogoutDto
    {
        public string LogoutId { get; set; }
    }

    public class LogoutViewModel : LogoutDto
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }

    public class LoggedOutViewModel : LogoutDto
    {
        public string PostLogoutRedirectUri { get; set; }
        public string ClientName { get; set; }
        public string SignOutIframeUrl { get; set; }
        public bool AutomaticRedirectAfterSignOut { get; set; }
    }
}
