using IdentityServer.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Objects
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UsernameError { get; set; }
        public string EmailError { get; set; }
        public string PasswordError { get; set; }
        public string PhoneNumberError { get; set; }
        public string ReturnUrl { get; set; }
        public long CaptchaId { get; set; }
        public PhotoCaptcha Captcha { get; set; }
        public string CaptchaAnswer { get; set; }
        public string CaptchaError { get; set; }

    }
}
