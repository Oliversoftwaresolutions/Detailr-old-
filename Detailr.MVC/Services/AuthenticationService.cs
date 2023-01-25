using AutoMapper;
using Detailr.MVC.Contracts;
using Detailr.MVC.Models;
using Detailr.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = Detailr.MVC.Contracts.IAuthenticationService;

namespace Detailr.MVC.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(IClient client, ILocalStorageService localStorageService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(localStorageService, client)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        ///  Authenticate user
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>Task<bool></returns>
        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authenticationRequest = new AuthRequest() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    // Get claims from token abd build auth user object
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

                    _localStorageService.SetStorageValue("token", authenticationResponse.Token);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  User registration
        /// </summary>
        /// <param name="register">Registration details</param>
        /// <returns>Task<bool></returns>
        public async Task<bool> Register(RegisterVM register)
        {
            RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(register);
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                await Authenticate(register.Email, register.Password);
                return true;
            }
            return false;
        }


        /// <summary>
        ///  Logout
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            _localStorageService.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// ParseClaims
        /// </summary>
        /// <param name="tokenContent">TokenContent</param>
        /// <returns></returns>
        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
