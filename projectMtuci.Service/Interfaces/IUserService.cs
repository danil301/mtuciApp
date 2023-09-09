using projectMtuci.Domain.Entity;
using projectMtuci.Domain.Response;
using projectMtuci.Domain.ViewModels.Login;
using projectMtuci.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(UserViewModel userViewModel);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
