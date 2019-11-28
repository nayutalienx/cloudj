using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using IdentityServer.Contracts;
using IdentityServer.DataAccessLayer;
using IdentityServer.Models;
using IdentityServer.Objects;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IdentityContext _identityContext;
        private readonly IRepository<PhotoCaptcha> _photoRepository;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IdentityContext identityContext,
            IRepository<PhotoCaptcha> photoRepository)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _identityContext = identityContext;
            _photoRepository = photoRepository;
        }

       

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            //partial
            if (returnUrl == null)
                return View("Error", new ErrorViewModel { RequestId = "Вы можете войти только с площадки" });
            return PartialView(await BuildLoginViewModelAsync(returnUrl));
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel model) {
            var data = await _photoRepository.GetAllAsync();
            IReadOnlyCollection<PhotoCaptcha> photo = data.ToList();
            PhotoCaptcha p = photo.First();
            return PartialView(new RegisterViewModel { ReturnUrl = model.ReturnUrl, CaptchaId = p.Id, Captcha = new PhotoCaptcha { Data = p.Data } });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterRequest(RegisterViewModel user, string button, CancellationToken cancellationToken = default)
        {
            if (!button.Equals("register"))
                return View("Register");


            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email))
                user.EmailError = "Enter email!";

            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(user.Password))
                user.PasswordError = "Enter password!";

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrWhiteSpace(user.Username))
                user.UsernameError = "Enter username!";

            if (string.IsNullOrEmpty(user.PhoneNumber) || string.IsNullOrWhiteSpace(user.PhoneNumber))
                user.PhoneNumberError = "Enter phone!";

            PhotoCaptcha capPhoto = await _photoRepository.GetAsync(user.CaptchaId);

            if (!capPhoto.Answer.Equals(user.CaptchaAnswer))
                user.CaptchaError = "Captcha was wrong!";

            user.Captcha = capPhoto;

            var user_email = await _userManager.FindByEmailAsync(user.Email);

            if (user_email != null)
                user.EmailError = "User with such email already exists";

            if (user.CaptchaError == null && user.EmailError == null && user.PasswordError == null && user.UsernameError == null && user.PhoneNumberError == null)
            {
                await _userManager.CreateAsync(_mapper.Map<IdentityUser>(user), user.Password);
                await _identityContext.SaveChangesAsync(cancellationToken);
                await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(user.Email), "User");
                return Redirect(user.ReturnUrl+"registration");
            }
            else
            {
                return PartialView("Register",user);
            }




        }






        [HttpGet]
        public IActionResult UserPanel() {
            if (User.IsInRole("Admin"))
                return View(_mapper.Map<List<UserViewModel>>(_userManager.Users.ToList()));
            else
                return Ok("Access denied!");
        }

        [HttpGet]
        public async Task<IActionResult> CaptchaPanel() 
        {
            if (!User.IsInRole("Admin"))
                return Ok("Access denied!");
            else {
                var data = await _photoRepository.GetAllAsync();
                IReadOnlyCollection<PhotoCaptcha> photo = data.ToList();
                ViewBag.Photo = photo;
                return View();
            }
                
        }

        [HttpPost]
        public async Task<IActionResult> CaptchaPanel(CaptchaModelView model)
        {
            byte[] p1 = null;
            using (var fs1 = model.Photo.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }

            await _photoRepository.AddAsync(new PhotoCaptcha { Data = p1, Answer = model.Answer});
            await _photoRepository.SaveChangesAsync();
            return Ok("Captcha added.");
        }

       [HttpGet]
        public IActionResult RolePanel() {
            if (User.IsInRole("Admin"))
                return View(_mapper.Map<List<RoleViewModel>>(_roleManager.Roles.ToList()));
            else
                return Ok("Access denied!");
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser() {
            if (!User.IsInRole("Admin"))
                return NotFound();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel user, CancellationToken cancellationToken = default)
        {
            if (!User.IsInRole("Admin"))
                return NotFound();
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return BadRequest("Enter all data");
            await _userManager.CreateAsync(_mapper.Map<IdentityUser>(user), user.Password);
            await _identityContext.SaveChangesAsync(cancellationToken);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole() {
            if (!User.IsInRole("Admin"))
                return NotFound();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel role, CancellationToken cancellationToken = default) {
            if (!User.IsInRole("Admin"))
                return NotFound();
            if (string.IsNullOrEmpty(role.Name))
                return BadRequest("Enter all data");
            await _roleManager.CreateAsync(new IdentityRole { Name = role.Name });

            await _identityContext.SaveChangesAsync(cancellationToken);
            return View();
        }

        [HttpGet]
        [Route("Account/DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id, CancellationToken cancellationToken = default) {
            if (!User.IsInRole("Admin"))
                return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            await _identityContext.SaveChangesAsync(cancellationToken);
            return Ok("Deleted.");
        }


        [HttpGet]
        [Route("Account/{id}")]
        public async Task<IActionResult> Info(string id, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.FindByIdAsync(id);
            return ApiResult(new UserInfoResult 
            { 
                Username = result.UserName,
                PhoneNumber = result.PhoneNumber,
                Email = result.Email
            });
        }


        [HttpGet]
        [Route("Account/EditUser/{id}")]
        public async Task<IActionResult> EditUser(string id) {
            var user = await _userManager.FindByIdAsync(id);
            var dto = _mapper.Map<EditUserViewModel>(user);
            var roles = await _userManager.GetRolesAsync(user);
            dto.UserRoles = roles.ToArray();
            dto.AllRoles = _roleManager.Roles.Select(x => x.Name).ToArray();
            return View(dto);
        }

        [HttpPost]
        [Route("Account/EditUser/{id}")]
        public async Task<IActionResult> EditUser(EditUserViewModel user,string id, CancellationToken cancellationToken = default)
        {
            var model = await _userManager.FindByIdAsync(id);
            model.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Password))
            {
                var hash_password = _userManager.PasswordHasher.HashPassword(model, user.Password);
                model.PasswordHash = hash_password;
            }
            model.UserName = user.Username;
            model.PhoneNumber = user.PhoneNumber;
            await _userManager.UpdateAsync(model);
            if (user.UserRoles != null)
            {
                await _userManager.RemoveFromRolesAsync(model, _roleManager.Roles.Select(x => x.Name).ToArray());
                await _userManager.AddToRolesAsync(model, user.UserRoles);
            }
            await _identityContext.SaveChangesAsync(cancellationToken);

            return Ok("Edited!");
        }

        [HttpPost]
        [Route("Account/ExternalEdit")]
        public async Task<IActionResult> ExternalEdit([FromBody]UserDto user, CancellationToken cancellationToken = default) {

            var model = await _userManager.FindByIdAsync(user.Id);
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            await _userManager.UpdateAsync(model);
            await _identityContext.SaveChangesAsync(cancellationToken);
            return ApiResult(new UserInfoResult
            {
                Username = user.Email,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            });
        }

        // to login 
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model, string button)
        {
            if (model?.Button != null)
                button = model.Button;

            // проверяем, находимся ли мы в контексте запроса на авторизацию
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (button != "login")
            {
                if (context != null)
                {
                    // возвращаем клиенту ответ об ошибке OIDC, в котором указано "отказано в доступе".
                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);
                    return Redirect(model.ReturnUrl);
                }

                return Redirect("~/");
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.ClientId));

                    if (context != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }

                    throw new Exception("invalid return URL");
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.ClientId));
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            var vm = await BuildLoginViewModelAsync(model);
            vm.ReturnUrl = model.ReturnUrl;
            vm.LoginError = "Неправильный логин или пароль";
            return PartialView(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                return await Logout(vm);
            }

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(LogoutDto model)
        {
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            return PartialView("LoggedOut", vm);
        }

        // to print view
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            string reg = (returnUrl.Contains("registration")) ? "registration was successful" : null;
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                    MessageFromRegistration = reg,
                    ExternalProviders = new ExternalProvider[] { new ExternalProvider { AuthenticationScheme = context.IdP } }

                };


                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                }).ToList();


            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;
                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = true,
                EnableLocalLogin = allowLocal,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                MessageFromRegistration = reg,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginDto model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = true };

            if (User?.Identity.IsAuthenticated != true)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = true,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }
                    }
                }
            }

            return vm;
        }
    }
}
