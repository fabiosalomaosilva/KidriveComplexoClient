using KidriveComplexoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidriveComplexoClient.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();

        Task<bool> GetAuthState(); 
        Task<string> GetNome();
        Task<string> GetAvatar();
        Task<string> GetEmail();
        Task<string> GetSetor();
        Task<string> GetCargo();

    }
}
